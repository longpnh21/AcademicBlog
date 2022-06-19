﻿using Application.Response;
using Application.Response.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Blogs
{
    public class GetBlogWithIdCommand : IRequest<Response<BlogResponse>>
    {
        public int BlogId { get; set; }
    }
}
