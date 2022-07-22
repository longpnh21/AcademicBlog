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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AcademicBlogContext context) : base(context) { }

        public virtual async Task<PaginatedList<User>> GetWithPaginationAsync(
            int pageIndex = 1,
            int pageSize = 50,
            List<Expression<Func<User, bool>>> filter = null,
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null,
            string includeProperties = "",
            bool? isDelete = false)
        {
            var query = _context.Set<User>().AsQueryable().AsNoTracking();
            if (isDelete != null)
            {
                query = query.Where(e => e.IsDeleted == isDelete.Value);
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

            return await PaginatedList<User>.ToPaginatedList(query, pageIndex, pageSize);
        }

        public async Task<User> GetByIdAsync(string id, bool isDeleted = false)
            => isDeleted ? await _context.Set<User>().FirstOrDefaultAsync(e => e.Id.Equals(id))
                : await _context.Set<User>().FirstOrDefaultAsync(e => e.Id.Equals(id) && !e.IsDeleted);

        public override async Task DeleteAsync(User entity)
        {
            entity.IsDeleted = true;
            _context.Set<User>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserStatus(User entity)
        {
            entity.IsDeleted = false;
            _context.Set<User>().Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}
