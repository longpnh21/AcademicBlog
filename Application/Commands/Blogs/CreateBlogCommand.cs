﻿using Application.Response;
using Application.Response.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Blogs
{
    public class CreateBlogCommand : IRequest<Response<BlogResponse>>
    {
        [Required]
        public string Content { get; set; }
        public string CreatorId { get; set; }
        public IList<IFormFile> Media { get; set; }
        public IList<int> Categories { get; set; }
        public IList<int> Tags { get; set; }
    }
}
