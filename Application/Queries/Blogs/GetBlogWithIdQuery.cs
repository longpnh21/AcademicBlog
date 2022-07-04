using Application.Response;
using Application.Response.Base;
using MediatR;

namespace Application.Queries.Blogs
{
    public class GetBlogWithIdQuery : IRequest<Response<BlogResponse>>
    {
        public int BlogId { get; set; }
    }
}
