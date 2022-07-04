using Application.Response;
using Application.Response.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Blogs
{
    public class DeleteBlogCommand : IRequest<Response<BlogResponse>>
    {
        [Required]
        public int Id { get; set; }
    }
}
