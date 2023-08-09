using Courses_FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewUtility;
using System.Net;

namespace Courses_FrontEnd.Controllers
{
    public class StudentController : Controller
    {
        
        public IActionResult Index()
        {

            IEnumerable<StudentsVM> students = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7230/api/");
                // HTTP GET
                var responseTask = client.GetAsync("Student");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<StudentsVM>>();
                    readTask.Wait();

                    students = readTask.Result;
                }
                else
                {
                    students = Enumerable.Empty<StudentsVM>();

                    ModelState.AddModelError(string.Empty, "Not Found");
                }
            }
            return View(students);
        }

        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateStudent(StudentsVM student)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:7230/api/");

                        var postTask = client.PostAsJsonAsync("Student", student);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "An error occurred while creating the course.");
                        }
                    }
                }

                return View(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the course.");
            }


        }

        [HttpGet]
/*        [Authorize(SD.Admin_Role)]
*/
        public async Task<IActionResult> Edit(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7230/api/");

                var responseTask = await client.GetAsync($"Student/{id}");
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadFromJsonAsync<StudentsVM>();
                    if (readTask != null)
                    {

                        return View(readTask);
                    }
                }


                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentsVM student)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:7230/api/");

                        var putTask = await client.PutAsJsonAsync($"Student/{student.Id}", student);
                        if (putTask.IsSuccessStatusCode)
                        {

                            return RedirectToAction("Index");
                        }
                        else if (putTask.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {

                            return RedirectToAction("Index");
                        }
                        else
                        {

                            ModelState.AddModelError(string.Empty, "An error occurred while updating the course.");
                        }
                    }
                }


                return View(student);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while updating the course.");
            }
        }
/*        [Authorize(SD.Admin_Role)]
*/
        public IActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7230/api/");
                var deleteTask = client.DeleteAsync($"Student/{id}");
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    ModelState.AddModelError(string.Empty, "Course not found.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while deleting the course. You are not authrized.");
                }
            }


            return View("Index");
        }
    }
}
