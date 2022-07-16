using Application.Response;
using Application.Response.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Votes
{
    public class DeleteVoteCommand : IRequest<Response<VoteResponse>>
    {
        [Required]
        public int BlogId { get; set; }
        public string UserId { get; set; }
    }
}
