using Application.Response;
using Application.Response.Base;
using Core.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Queries.Tags
{
    public class GetTagWithPaginationQuery : IRequest<Response<PaginatedList<TagResponse>>>
    {
        [Range(1, int.MaxValue)]
        public int PageIndex { get; set; } = 1;
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = 10;
        public string SearchValue { get; set; }
    }
}
