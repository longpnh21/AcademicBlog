using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
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
    }
}
