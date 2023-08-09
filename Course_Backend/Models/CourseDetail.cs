using System.ComponentModel.DataAnnotations;

namespace Course_Backend.Models
{
    public class CourseDetail
    {
        [Key]
        
        public int Id { get; set; }

        [Required]
    /*    [Range(1,100)]*/
        public string CourseName { get; set;}

        [Required]
/*        [Range(1, 500)]
*/        public string CourseDiscription { get; set;}
            
        public DateOnly StartDate { get; set;}
        public DateOnly EndDate { get; set;}



        //adding a validation here.

        [Range(1,10)]
        public int AvailabeSeats { get; set;}



        public decimal? CoursePrice { get; set; }
        public string? Status { get; set; } 




    }
}
