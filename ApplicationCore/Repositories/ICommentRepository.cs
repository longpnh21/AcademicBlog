using Core.Entities;
using Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetAllReply(int id);
        Task<IEnumerable<Comment>> GetAllCommentFromPost(int id);
    }
}
