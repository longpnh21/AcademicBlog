﻿using Application.Response;
using Application.Response.Base;
using Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Tags
{
    public class GetTagWithPaginationQuery : IRequest<Response<PaginatedList<TagResponse>>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
