using Application.Response;
using Application.Response.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Queries.Comments
{
    public class GetCommentWithBlogIdQuery : IRequest<Response<IEnumerable<CommentResponse>>>
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
    }
}
