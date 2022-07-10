using Core.Entities;
using Core.Repositories.Base;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByIdAsync(string id);
    }
}
