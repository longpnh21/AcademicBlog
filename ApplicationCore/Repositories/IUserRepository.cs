using Core.Common;
using Core.Entities;
using Core.Repositories.Base;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<PaginatedList<User>> GetWithPaginationAsync(
            int pageIndex = 1,
            int pageSize = 50,
            Expression<Func<User, bool>> filter = null,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null,
            string includeProperties = "",
            bool isDelete = false);
        Task<User> GetByIdAsync(string id, bool isDeleted = false);

        Task UpdateUserStatus(User entity);

    }
}
