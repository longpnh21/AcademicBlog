using Application.Response;
using Application.Response.Base;
using Core.Common;
using MediatR;

namespace Application.Queries.Tags
{
    public class GetTagWithPaginationQuery : IRequest<Response<PaginatedList<TagResponse>>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
