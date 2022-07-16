using Application.Commands.Votes;
using Application.Mappers;
using Application.Response;
using Application.Response.Base;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Votes
{
    public class CreateVoteCommandHandler : IRequestHandler<CreateVoteCommand, Response<VoteResponse>>
    {
        private readonly IVoteRepository _voteRepository;

        public CreateVoteCommandHandler(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public async Task<Response<VoteResponse>> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<VoteResponse>();
            try
            {
                var entity = AcademicBlogMapper.Mapper.Map<Vote>(request);
                if (entity is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                var newVote = await _voteRepository.AddAsync(entity);

                response = new Response<VoteResponse>(AcademicBlogMapper.Mapper.Map<VoteResponse>(newVote))
                {
                    StatusCode = HttpStatusCode.Created
                };
            }
            catch (ApplicationException ex)
            {
                response = new Response<VoteResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.UnprocessableEntity
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

