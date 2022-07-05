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
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public string CreatorId { get; set; }
        public IList<IFormFile> Media { get; set; }
    }
}
