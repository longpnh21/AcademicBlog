using Application.Response;
using Application.Response.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Comments
{
    public class CreateCommentCommand : IRequest<Response<CommentResponse>>
    {
        [Required]
        public int BlogId { get; set; }
        [MaxLength(1000)]
        [Required]
        public string Content { get; set; }
        [Required]
        public string UserId { get; set; }
        public int? ReferenceId { get; set; }
    }
}
