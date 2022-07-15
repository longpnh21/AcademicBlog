using Core.Common;
using Core.Entities;
using Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IBlogRepository : IRepository<Category>
    {
        Task<Category> GetByIdAsync(int id, string includeProperties = "");
        Task<PaginatedList<Category>> SearchAsync(string searchValue,
            int pageIndex = 1,
            int pageSize = 50,
            List<Expression<Func<Category, bool>>> filter = null,
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null,
            string includeProperties = "");
    }
}
