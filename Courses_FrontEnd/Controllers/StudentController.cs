using Courses_FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Courses_FrontEnd.Controllers
{
    public class StudentController : Controller
    {
        [Authorize]
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

    }
}
