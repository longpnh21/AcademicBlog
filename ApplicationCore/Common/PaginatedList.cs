using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Common
{
    public class PaginatedList<T> : List<T>
    {

        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList()
        {

        }

        public PaginatedList(IEnumerable<T> source, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            AddRange(source);
        }

        public static async Task<PaginatedList<T>> ToPaginatedList(IQueryable<T> query, int pageIndex, int pageSize)
        {
            int count = await query.CountAsync();
            var result = await query.ToListAsync();
            return new PaginatedList<T>(result, count, pageIndex, pageSize);
        }

        public bool HasPreviousPage => PageIndex > 0;

        public bool HasNextPage => PageIndex + 1 < TotalPages;
    }
}
