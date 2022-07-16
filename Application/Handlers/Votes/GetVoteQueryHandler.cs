using Application.Mappers;
using Application.Queries.Votes;
using Application.Response;
using Application.Response.Base;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Votes
{
    public class GetVoteQueryHandler : IRequestHandler<GetVoteQuery, Response<List<VoteResponse>>>
    {
        private readonly IVoteRepository _voteRepository;

        public GetVoteQueryHandler(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public async Task<Response<List<VoteResponse>>> Handle(GetVoteQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<VoteResponse>>();

            try
            {
                List<Expression<Func<Vote, bool>>> filter = new();
                filter.Add(e => e.BlogId == request.BlogId);
                if (request.Type is not null)
                {
                    filter.Add(e => e.BlogId == request.BlogId && e.Type == request.Type);
                }

                var result = await _voteRepository.GetAllAsync(filter);
                var mappedResult = AcademicBlogMapper.Mapper.Map<IEnumerable<Vote>, List<VoteResponse>>(result);

                response = new Response<List<VoteResponse>>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                response = new Response<List<VoteResponse>>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            return response;
        }
    }
}
