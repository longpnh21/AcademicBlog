using Application.Response;
using Application.Response.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Tags
{
    public class DeleteTagCommand : IRequest<Response<TagResponse>>
    {
        [Required]
        public int TagId { get; set; }
    }
}
