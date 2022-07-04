﻿using Application.Response;
using Application.Response.Base;
using Core.Common;
using MediatR;

namespace Application.Queries.Blogs
{
    public class GetBlogWithPaginationQuery : IRequest<Response<PaginatedList<BlogResponse>>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
