using Application.Response;
using Application.Response.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Blogs
{
    public class ApproveBlogCommand : IRequest<Response<BlogResponse>>
    {
        [Required]
        public int BlogId { get; set; }
        public string ApproverId { get; set; }
    }
}
