using Application.Response;
using Application.Response.Base;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users
{
    public class GetUserWithPaginationQuery : IRequest<Response<PaginatedList<UserResponse>>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
