using Core.Entities;
using Core.Repositories.Base;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<Blog> GetByIdAsync(int id, string includeProperties = "");
    }
}
