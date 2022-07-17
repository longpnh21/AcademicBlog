using Core.Common;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BlogRepository : BaseRepository<Blog>, IBlogRepository
    {
        public BlogRepository(AcademicBlogContext context) : base(context) { }

        public async Task<Blog> GetByIdAsync(int id, string includeProperties = "")
        {
            var query = _context.Set<Blog>().AsQueryable().AsNoTracking();

            foreach (string includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<PaginatedList<Blog>> SearchAsync(string searchValue,
            int pageIndex = 1,
            int pageSize = 50,
            List<Expression<Func<Blog, bool>>> filter = null,
            Func<IQueryable<Blog>, IOrderedQueryable<Blog>> orderBy = null,
            string includeProperties = "")
        {
            var query = _context.Set<Blog>().AsQueryable().AsNoTracking().Include(e => e.BlogCategories).ThenInclude(b => b.Category).Include(e => e.BlogTags).ThenInclude(e => e.Tag).AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                query = query.Where(e => e.BlogCategories.Any(x => x.Category.Name.Contains(searchValue) || e.BlogTags.Any(x => x.Tag.Name.Contains(searchValue))));
            }
            if (filter is not null)
            {
                foreach (var exp in filter)
                {
                    query = query.Where(exp);
                }
            }

            foreach (string includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy is not null)
            {
                query = orderBy(query);
            }

            return await PaginatedList<Blog>.ToPaginatedList(query, pageIndex, pageSize);
        }
    }
}
