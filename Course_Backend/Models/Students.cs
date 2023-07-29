using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Course_Backend.Models
{
    public class Students
    {


        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }


        
        public int? CourseId { get; set; }

        [ForeignKey("CourseId")] 
        public CourseDetail? CourseDetail { get; set; }

        public decimal? TotalPriceOfCourses { get; set; }
    }

}
