
using Course_Backend.Models;
using Course_Backend.Repository;
using Course_Backend.Repository.IRepository;
using Courses_FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Course_Backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _course;

        public CourseController(ICourseRepository course)
        {
            _course = course;
        }

        [HttpGet]
        public async Task<ActionResult<List<CourseDetail>>> GetAllCourses()
        {
            try
            {
                var courses = await _course.GetAllAsync();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching courses.");
            }
        }




        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDetail>> GetCourseById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Id. Please provide a valid Course Id.");
                }

                var course = await _course.GetAsync(id);

                if (course == null)
                {
                    return NotFound($"Course with Id {id} not found.");
                }

                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the course details.");
            }
        }

        [HttpDelete("{id}")]
       
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Id. Please provide a valid Course Id.");
                }

                var course = await _course.GetAsync(id);

                if (course == null)
                {
                    return NotFound($"Course with Id {id} not found.");
                }

                await _course.RemoveAsync(course);
                await _course.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the course.");
            }
        }



        [HttpPost]

        
        public async Task<IActionResult> CreateCourse([FromBody] CourseDetail newCourse)
        {
            try
            {
                if (newCourse == null)
                {
                    return BadRequest("Invalid request. Please provide valid course details.");
                }

                newCourse.Status = NewUtility.SD.StatusPending;

                await _course.CreateAsync(newCourse);
                await _course.SaveAsync();

               
                return CreatedAtAction("GetCourseById", new { id = newCourse.Id }, newCourse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the course.");
            }
        }

        [HttpPut("{id}")]
       

        public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseDetail updatedCourse)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Id. Please provide a valid Course Id.");
                }

                if (updatedCourse == null)
                {
                    return BadRequest("Invalid request. Please provide valid course details.");
                }

                var existingCourse = await _course.GetAsync(id);

                if (existingCourse == null)
                {
                    return NotFound($"Course with Id {id} not found.");
                }
                if (existingCourse.Id != updatedCourse.Id)
                {

                    return BadRequest("Ids Mismatch");
                }
                else
                {
                    existingCourse.CourseName = updatedCourse.CourseName;
                    existingCourse.CourseDiscription = updatedCourse.CourseDiscription;
                    existingCourse.StartDate = updatedCourse.StartDate;
                    existingCourse.EndDate = updatedCourse.EndDate;
                    existingCourse.AvailabeSeats = updatedCourse.AvailabeSeats;
                    existingCourse.Status = NewUtility.SD.StatusApproved;

                }
                await _course.UpdateAsync(existingCourse);
                await _course.SaveAsync();

                return Ok(existingCourse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the course.");
            }
        }

       
     
    }
}
