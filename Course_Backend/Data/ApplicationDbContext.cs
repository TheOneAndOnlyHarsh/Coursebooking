using Course_Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Course_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<CourseDetail> CourseDetails { get; set; }
        public DbSet<Students> Students { get; set; }

        public DbSet<LocalUser> LocalUsers { get; set; }





    }
}
