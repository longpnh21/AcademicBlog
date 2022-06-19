using Application.Response;
using Application.Response.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Tags
{
    public class GetTagWithIdQuery : IRequest<Response<TagResponse>>
    {
        public int TagId { get; set; }
    }
}
