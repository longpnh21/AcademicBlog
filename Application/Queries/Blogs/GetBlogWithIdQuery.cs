using Application.Response;
using Application.Response.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Queries.Blogs
{
    public class GetBlogWithIdQuery : IRequest<Response<BlogResponse>>
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
    }
}
