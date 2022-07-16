using Application.Response;
using Application.Response.Base;
using Core.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Votes
{
    public class EditVoteCommand : IRequest<Response<VoteResponse>>
    {
        [Required]
        public int BlogId { get; set; }
        public string UserId { get; set; }
        public VoteType Type { get; set; }
    }
}
