using Application.Response;
using Application.Response.Base;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Comments
{
    public class DeleteCommentCommand : IRequest<Response<CommentResponse>>
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
    }
}
