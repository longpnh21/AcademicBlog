using Application.Response;
using Application.Response.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Tags
{
    public class CreateTagCommand : IRequest<Response<TagResponse>>
    {
        [Required]
        public string Name { get; set; }
    }
}
