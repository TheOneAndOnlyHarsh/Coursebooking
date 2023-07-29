using Microsoft.AspNetCore.Mvc;

namespace Courses_FrontEnd.Controllers
{
    public class EnrollmentController : Controller
    {
        public async Task<IActionResult> Enroll(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7230/");

                var response = await client.GetAsync($"Enroll?courseId={id}");

                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("EnrollmentSuccess");
                }
                else
                {
                    // Failed enrollment
                    return RedirectToAction("EnrollmentFailure");
                }
            }
        }

        public IActionResult EnrollmentSuccess(int courseId)
        {

            return View(courseId);
        }

        public IActionResult EnrollmentFailure(int courseId)
        {

            return View(courseId);
        }

    }
}
