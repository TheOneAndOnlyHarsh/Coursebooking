using AutoMapper;
using Course_Backend.Data;
using Course_Backend.Models;
using Course_Backend.Models.DTO;
using Course_Backend.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Course_Backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _student;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository student , ApplicationDbContext db , IMapper mapper)
        {
            _student = student;
            _db = db;
            _mapper = mapper;
        }

        //this is just for the admin:
        [HttpGet]
        public async Task<IEnumerable<Students>> GetAllAsync()
        {
            return await _db.Students
                .Include(s => s.CourseDetail)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = _db.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }


        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] StudentsCreateDTO student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

          
            bool courseIdExists = await _db.CourseDetails.AnyAsync(c => c.Id == student.CourseId);
            if (!courseIdExists)
            {
                return BadRequest("Invalid CourseId. Please provide a valid CourseId.");
            }

            var studentEntity = _mapper.Map<Students>(student);


            _db.Students.Add(studentEntity);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = studentEntity.Id }, studentEntity);
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteStudent(int id)
        {
            var student = _db.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            _db.Students.Remove(student);
            _db.SaveChanges();

            return NoContent(); 
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentsCreateDTO updatedStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != updatedStudent.Id)
            {
                return BadRequest("Ids Not matched");


            }

            bool courseIdExists = await _db.CourseDetails.AnyAsync(c => c.Id == updatedStudent.CourseId);
            if (!courseIdExists)
            {
                return BadRequest("Invalid CourseId. Please provide a valid CourseId.");
            }

            var existingStudent = await _db.Students.FindAsync(id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedStudent, existingStudent);


            _db.Students.Update(existingStudent);
            await _db.SaveChangesAsync();

            return Ok(existingStudent);
        }


    }
}
