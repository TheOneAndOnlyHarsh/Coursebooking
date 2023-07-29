using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Course_Backend.Models.DTO
{
    public class StudentsCreateDTO
    {
        [Key]
        
        public int Id { get; set; }

        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int? CourseId { get; set; }

        public decimal? TotalPriceOfCourses { get; set; }



    }

}
