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
    public class EditVoteCommandHandler : IRequestHandler<EditVoteCommand, Response<VoteResponse>>
    {
        private readonly IVoteRepository _voteRepository;

        public EditVoteCommandHandler(IVoteRepository tagRepository)
        {
            _voteRepository = tagRepository;
        }

        public async Task<Response<VoteResponse>> Handle(EditVoteCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<VoteResponse>();
            try
            {
                var entity = AcademicBlogMapper.Mapper.Map<Vote>(request);

                if (entity is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }
                var e = await _voteRepository.GetByIdAsync(new object[] { request.UserId, request.BlogId });
                if (e is null)
                {
                    await _voteRepository.AddAsync(entity);
                }
                else
                {
                    await _voteRepository.UpdateAsync(entity);
                }
                response = new Response<VoteResponse>()
                {
                    StatusCode = HttpStatusCode.NoContent,
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
