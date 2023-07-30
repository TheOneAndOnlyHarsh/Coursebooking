using Courses_FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Courses_FrontEnd.Controllers
{
    public class EnrollmentController : Controller
    {
        [Authorize]

        public async Task<IActionResult> Enroll(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7230/");

                var response = await client.GetAsync($"Enroll?courseId={id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("EnrollmentSuccess", new { courseId = id });
                }
                else
                {
                    // Failed enrollment
                    return RedirectToAction("EnrollmentFailure", new { courseId = id });
                }
            }
        }

       
        public IActionResult EnrollmentSuccess(int courseId)
        {
            CourseVM courseViewModel = new CourseVM
            {
                Id = courseId
                // Set other properties as needed.
            };

            return View(courseViewModel);
        }

        public IActionResult EnrollmentFailure(int courseId)
        {
            return View(courseId);
        }

        [HttpGet]
        [Route("Enrollment/Unenrollment/{id}")]
        public async Task<IActionResult> Unenrollment(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7230/");

                var response = await client.GetAsync($"Unenroll?courseId={id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("UnenrollmentSuccess", new { courseId = id });
                }
                else
                {
                    return RedirectToAction("UnenrollmentFailure", new { courseId = id });
                }
            }
        }

        public IActionResult UnenrollmentSuccess(int courseId)
        {
            return View(courseId);
        }
    }
}
