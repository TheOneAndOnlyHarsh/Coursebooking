namespace Courses_FrontEnd.Models
{
    public class CourseVM
    {
        public int Id { get; set; }

        public string CourseName { get; set; }
        public string CourseDiscription { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int? AvailabeSeats { get; set; }

        public string? Status { get; set; }
        
        public string? ImageUrl { get; set; }   
    }
}
