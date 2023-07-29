using Course_Backend.Data;
using Course_Backend.Models;
using Course_Backend.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course_Backend.Repository
{
    public class StudentRepository : Repository<Students>, IStudentRepository
    {
        private readonly ApplicationDbContext _db;
        public StudentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Students> UpdateAsync(Students entity)
        {
            _db.Update(entity);
            await SaveAsync();
            return entity;
        }
        public async Task<IEnumerable<Students>> GetAllAsync(string include)
        {
            IQueryable<Students> query = _db.Students;

            if (!string.IsNullOrEmpty(include))
            {
                var includes = include.Split(',');

                foreach (var inc in includes)
                {
                    query = query.Include(inc);
                }
            }

            return await query.ToListAsync();
        }





    }
}
