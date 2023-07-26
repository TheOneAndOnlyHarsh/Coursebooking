using Course_Backend.Models;

namespace Course_Backend.Repository.IRepository
{
    public interface ICourseRepository : IRepository<CourseDetail>
    {
        Task<CourseDetail> UpdateAsync(CourseDetail entity);
    }
}
