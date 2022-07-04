using Application.Response;
using Application.Response.Base;
using Core.Common;
using MediatR;

namespace Application.Queries.Categories
{
    public class GetCategoryWithPaginationQuery : IRequest<Response<PaginatedList<CategoryResponse>>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
