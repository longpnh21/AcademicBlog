using Application.Response;
using Application.Response.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Blogs
{
    public class ApproveBlogCommand : IRequest<Response<BlogResponse>>
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        public string ApproverId { get; set; }
    }
}
