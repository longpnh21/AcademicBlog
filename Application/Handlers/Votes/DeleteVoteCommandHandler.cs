using Application.Commands.Votes;
using Application.Response;
using Application.Response.Base;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Votes
{
    public class DeleteVoteCommandHandler : IRequestHandler<DeleteVoteCommand, Response<VoteResponse>>
    {
        private readonly IVoteRepository _voteRepository;

        public DeleteVoteCommandHandler(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public async Task<Response<VoteResponse>> Handle(DeleteVoteCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<VoteResponse>();
            try
            {
                var result = await _voteRepository.GetByIdAsync(new object[] {request.BlogId});
                if (result is not null)
                {
                    await _voteRepository.DeleteAsync(result);
                }
               
                response = new Response<VoteResponse>()
                {
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            catch (Exception ex)
            {
                response = new Response<VoteResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            return response;
        }
    }
}
