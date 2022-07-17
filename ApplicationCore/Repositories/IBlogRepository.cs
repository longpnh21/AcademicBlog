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
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<Blog> GetByIdAsync(int id, string includeProperties = "");
        Task<PaginatedList<Blog>> SearchAsync(string searchValue,
            int pageIndex = 1,
            int pageSize = 50,
            List<Expression<Func<Blog, bool>>> filter = null,
            Func<IQueryable<Blog>, IOrderedQueryable<Blog>> orderBy = null,
            string includeProperties = "");
    }
}
