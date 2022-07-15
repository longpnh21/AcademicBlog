using Application.Mappers;
using Application.Queries.Tags;
using Application.Response;
using Application.Response.Base;
using Core.Common;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Tags
{
    public class GetTagWithPaginationHandler : IRequestHandler<GetTagWithPaginationQuery, Response<PaginatedList<TagResponse>>>
    {
        private readonly ITagRepository _tagRepository;

        public GetTagWithPaginationHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Response<PaginatedList<TagResponse>>> Handle(GetTagWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<PaginatedList<TagResponse>>();

            try
            {
                var filter = new List<Expression<Func<Tag, bool>>>();
                if (!string.IsNullOrWhiteSpace(request.SearchValue))
                {
                    filter.Add(e => e.Name.Contains(request.SearchValue));
                }
                var result = await _tagRepository.GetWithPaginationAsync(request.PageIndex, request.PageSize, filter: filter);
                var mappedResult = AcademicBlogMapper.Mapper.Map<PaginatedList<Tag>, PaginatedList<TagResponse>>(result);

                response = new Response<PaginatedList<TagResponse>>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                response = new Response<PaginatedList<TagResponse>>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            return response;
        }
    }
}
