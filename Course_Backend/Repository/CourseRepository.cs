using Course_Backend.Data;
using Course_Backend.Models;
using Course_Backend.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Course_Backend.Repository
{
    public class CourseRepository : Repository<CourseDetail>, ICourseRepository
    {
        private readonly ApplicationDbContext _db;
        public CourseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<CourseDetail> UpdateAsync(CourseDetail entity)
        {
            _db.Update(entity);
            await SaveAsync();
            return entity;
        }

    }
}
