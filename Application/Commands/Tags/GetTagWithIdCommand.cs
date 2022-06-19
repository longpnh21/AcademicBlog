using Application.Response;
using Application.Response.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Tags
{
    public class GetTagWithIdCommand : IRequest<Response<TagResponse>>
    {
        public int TagId { get; set; }
    }
}
