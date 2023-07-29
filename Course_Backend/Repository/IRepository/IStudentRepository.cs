using Course_Backend.Models;

namespace Course_Backend.Repository.IRepository
{
    public interface IStudentRepository : IRepository<Students>
    {
        Task<Students> UpdateAsync(Students entity);
        Task<IEnumerable<Students>> GetAllAsync(string include);

    }
}
