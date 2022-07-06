using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AcademicBlogContext context) : base(context) { }

        public async Task<User> GetByIdAsync(string id)
            => await _context.Set<User>().FindAsync(id);
    }
}
