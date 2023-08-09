using AspNetCoreHero.ToastNotification.Abstractions;
using Courses_FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewUtility;
using System.Net;
using System.Net.Http;

namespace Courses_FrontEnd.Controllers
{
   


    public class CourseController : Controller
    {


        
        public IActionResult Index()
        {
            IEnumerable<CourseVM> courses = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7230/api/");
                
                var responseTask = client.GetAsync("Course");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<CourseVM>>();
                    readTask.Wait();

                    courses = readTask.Result;
                }
                else
                {


                    courses = Enumerable.Empty<CourseVM>();

                    ModelState.AddModelError(string.Empty, "Not Found");
                }
            }
            return View(courses);
        }


        public IActionResult Details(int id)
        {
            CourseVM course = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7230/api/");
                
                var responseTask = client.GetAsync($"Course/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<CourseVM>();
                    readTask.Wait();

                    course = readTask.Result;
                }
                else
                {
                    course = null;
                    ModelState.AddModelError(string.Empty, "Course not found");
                }
            }

            if (course == null)
            {
                return RedirectToAction("Index");
            }

            return View(course);
        }

       /* [Authorize(Roles = "Admin")]*/
        public IActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7230/api/");
                var deleteTask = client.DeleteAsync($"Course/{id}");
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


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
/*        [Authorize(Roles = "Admin")]
*/
        public async Task<IActionResult> CreateCourse(CourseVM course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:7230/api/");

                        var postTask = client.PostAsJsonAsync("Course", course);
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
                
                return View(course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the course.");
            }


        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7230/api/");

                var responseTask = await client.GetAsync($"Course/{id}");
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadFromJsonAsync<CourseVM>();
                    if (readTask != null)
                    {
                        
                        return View(readTask);
                    }
                }

                
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CourseVM updatedCourse)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:7230/api/");

                        var putTask = await client.PutAsJsonAsync($"Course/{updatedCourse.Id}", updatedCourse);
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

               
                return View(updatedCourse);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while updating the course.");
            }
        }

        [HttpGet]

        public IActionResult PendingList()
        {
            IEnumerable<CourseVM> pendingCourses = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7230/api/");

                var responseTask = client.GetAsync("Course");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<CourseVM>>();
                    readTask.Wait();

                    var allCourses = readTask.Result;
                    pendingCourses = allCourses.Where(course => course.Status == "Pending");
                }
                else
                {
                    pendingCourses = Enumerable.Empty<CourseVM>();

                    ModelState.AddModelError(string.Empty, "Not Found");
                }
            }

            return View(pendingCourses);
        }
        public IActionResult ApprovedList()
        {
            IEnumerable<CourseVM> ApprovedCourses = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7230/api/");

                var responseTask = client.GetAsync("Course");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<CourseVM>>();
                    readTask.Wait();

                    var allCourses = readTask.Result;
                    ApprovedCourses = allCourses.Where(course => course.Status == "Approved");
                }
                else
                {
                    ApprovedCourses = Enumerable.Empty<CourseVM>();

                    ModelState.AddModelError(string.Empty, "Not Found");
                }
            }

            return View(ApprovedCourses);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveCourse(int courseId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7230/api/");

                    var response = await client.PutAsync($"Course/approve/{courseId}", null);

                    if (response.IsSuccessStatusCode)
                    {
                        // Show success notification or message
                        TempData["success"] = "Course status approved successfully.";
                    }
                    else
                    {
                        // Show error notification or message
                        TempData["error"] = "An error occurred while approving the course status.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                TempData["error"] = "An error occurred while approving the course status.";
            }

            return RedirectToAction("PendingList"); // Redirect to the PendingList action
        }

    }
}