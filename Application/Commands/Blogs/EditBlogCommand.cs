using Application.Response;
using Application.Response.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Blogs
{
    public class EditBlogCommand : IRequest<Response<BlogResponse>>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public int CreatorId { get; set; }
        public IList<IFormFile> Media { get; set; }
    }
}
