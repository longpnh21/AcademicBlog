using Application.Response;
using Application.Response.Base;
using Core.Enums;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Queries.Votes
{
    public class GetVoteQuery : IRequest<Response<List<VoteResponse>>>
    {
        [Required]
        public int BlogId { get; set; }
        public VoteType? Type { get; set; }
    }
}
