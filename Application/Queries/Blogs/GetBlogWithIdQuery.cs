using Application.Response;
using Application.Response.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Blogs
{
    public class GetBlogWithIdQuery : IRequest<Response<BlogResponse>>
    {
        public int BlogId { get; set; }
    }
}
