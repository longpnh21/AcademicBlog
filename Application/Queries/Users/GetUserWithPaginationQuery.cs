using Application.Response;
using Application.Response.Base;
using Core.Common;
using MediatR;

namespace Application.Queries.Users
{
    public class GetUserWithPaginationQuery : IRequest<Response<PaginatedList<UserResponse>>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
