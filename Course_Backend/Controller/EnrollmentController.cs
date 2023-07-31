using Course_Backend.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Course_Backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {

        private readonly ICourseRepository _course;

        public EnrollmentController(ICourseRepository course)
        {
            _course = course;
        }

        [HttpGet]
        [Route("/Enroll")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> EnrollCourse(int courseId)
        {
            try
            {
                var course = await _course.GetAsync(courseId);
                if (course == null)
                {
                    return NotFound();
                }

                if (course.AvailabeSeats <= 0)
                {
                    return BadRequest("Course is full, cannot enroll.");
                }

                course.AvailabeSeats--;
                await _course.UpdateAsync(course);

                return Ok("Enrolled successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while enrolling in the course.");
            }

        }
        [HttpGet]
        [Route("/Unenroll")]
        [Authorize(Roles = "Student")]

        public async Task<IActionResult> UnenrollCourse(int courseId)
        {
            try
            {
                var course = await _course.GetAsync(courseId);
                if (course == null)
                {
                    return NotFound();
                }

               

                course.AvailabeSeats++;
                await _course.UpdateAsync(course);

                return Ok("Unenrolled successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while unenrolling from the course.");
            }
        }
    }
}
