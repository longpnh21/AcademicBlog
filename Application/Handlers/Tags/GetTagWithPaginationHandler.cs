using Application.Mappers;
using Application.Queries.Tags;
using Application.Response;
using Application.Response.Base;
using Core.Common;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
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
                if (request.PageIndex <= 0 || request.PageSize <= 0)
                {
                    throw new ArgumentException("Invalid request");
                }
                var result = await _tagRepository.GetWithPaginationAsync(request.PageIndex, request.PageSize);
                var mappedResult = AcademicBlogMapper.Mapper.Map<PaginatedList<Tag>, PaginatedList<TagResponse>>(result);
                response = new Response<PaginatedList<TagResponse>>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (ArgumentException ex)
            {
                response = new Response<PaginatedList<TagResponse>>(ex.Message)
                {
                    StatusCode = HttpStatusCode.BadRequest,
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
