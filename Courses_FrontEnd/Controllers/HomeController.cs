using Courses_FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Courses_FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var courses = new List<CourseVM>
         {
            new CourseVM { CourseName = "Oops with Java", ImageUrl = "https://www.wemakescholars.com/uploads/blog/TopprofessionalITcoursetopursueincollege.jpg"  },
            new CourseVM { CourseName = "Ethical Hacking", ImageUrl = "https://www.wemakescholars.com/uploads/blog/TopprofessionalITcoursetopursueincollege.jpg"  },
            new CourseVM { CourseName = "Mastering.net", ImageUrl = "https://www.wemakescholars.com/uploads/blog/TopprofessionalITcoursetopursueincollege.jpg"  },

         };
            return View(courses);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}