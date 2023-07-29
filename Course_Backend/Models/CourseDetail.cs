using System.ComponentModel.DataAnnotations;

namespace Course_Backend.Models
{
    public class CourseDetail
    {
        [Key]
        
        public int Id { get; set; }
        public string CourseName { get; set;} 
        public string CourseDiscription { get; set;}
            
        public DateOnly StartDate { get; set;}
        public DateOnly EndDate { get; set;}
        public int AvailabeSeats { get; set;}

        public decimal? CoursePrice { get; set; }
        public string? Status { get; set; } 




    }
}
