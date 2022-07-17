using Application.Response;
using Application.Response.Base;
using Core.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Queries.Categories
{
    public class GetCategoryWithPaginationQuery : IRequest<Response<PaginatedList<CategoryResponse>>>
    {
        [Range(1, int.MaxValue)]
        public int PageIndex { get; set; } = 1;
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = 10;
        public string SearchValue { get; set; }
    }
}
