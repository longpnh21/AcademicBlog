using Application.Response;
using Application.Response.Base;
using Core.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Blogs
{
    public class ApproveBlogCommand : IRequest<Response<BlogResponse>>
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        public BlogStatus Status { get; set; } = BlogStatus.Available;
        public string ApproverId { get; set; }
    }
}
