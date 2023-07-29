using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Course_Backend.Models
{
    public class PurchasedCourse
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Students Student { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public CourseDetail Courses { get; set; }

        public decimal CoursePrice { get; set; }

    }
}

