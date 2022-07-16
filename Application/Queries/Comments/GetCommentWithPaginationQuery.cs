using Application.Response;
using Application.Response.Base;
using Core.Common;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Queries.Comments
{
    public class GetCommentWithPaginationQuery : IRequest<Response<PaginatedList<CommentResponse>>>
    {
        [Range(1, int.MaxValue)]
        public int PageIndex { get; set; } = 1;
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = 10;
    }
}
