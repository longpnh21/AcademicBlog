using Core.Common;
using Core.Repositories.Base;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Base
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly AcademicBlogContext _context;
        public BaseRepository(AcademicBlogContext context)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _context = context;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(
            List<Expression<Func<T, bool>>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            var query = _context.Set<T>().AsQueryable().AsNoTracking();

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

            return await query.ToListAsync();
        }

        public virtual async Task<PaginatedList<T>> GetWithPaginationAsync(
            int pageIndex = 1,
            int pageSize = 50,
            List<Expression<Func<T, bool>>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            var query = _context.Set<T>().AsQueryable().AsNoTracking();
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

            return await PaginatedList<T>.ToPaginatedList(query, pageIndex, pageSize);
        }

        public virtual async Task<T> GetByIdAsync(object[] id)
            => await _context.Set<T>().FindAsync(id);

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
