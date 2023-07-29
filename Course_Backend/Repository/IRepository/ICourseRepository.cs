using Course_Backend.Models;

namespace Course_Backend.Repository.IRepository
{
    public interface ICourseRepository : IRepository<CourseDetail>
    {
/*        object FirstOrDefaultAsync(Func<object, bool> value);
*/        Task<CourseDetail> UpdateAsync(CourseDetail entity);
    }
}
