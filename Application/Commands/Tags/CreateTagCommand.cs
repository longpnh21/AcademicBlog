using Application.Response;
using Application.Response.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Tags
{
    public class CreateTagCommand : IRequest<Response<TagResponse>>
    {
        [Required]
        public string Name { get; set; }
    }
}
