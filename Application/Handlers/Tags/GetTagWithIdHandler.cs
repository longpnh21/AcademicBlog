using Application.Mappers;
using Application.Queries.Tags;
using Application.Response;
using Application.Response.Base;
using Core.Repositories;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Tags
{
    public class GetTagWithIdHandler : IRequestHandler<GetTagWithIdQuery, Response<TagResponse>>
    {
        private readonly ITagRepository _tagRepository;

        public GetTagWithIdHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;

        }

        public async Task<Response<TagResponse>> Handle(GetTagWithIdQuery query, CancellationToken cancellationToken)
        {
            var response = new Response<TagResponse>();

            try
            {
                var result = await _tagRepository.GetByIdAsync(new object[] { query.Id });
                if (result is null)
                {
                    throw new NullReferenceException("Not found tag");
                }
                var mappedResult = AcademicBlogMapper.Mapper.Map<TagResponse>(result);
                if (mappedResult is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                response = new Response<TagResponse>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (NullReferenceException ex)
            {
                response = new Response<TagResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
            catch (ApplicationException ex)
            {
                response = new Response<TagResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.UnprocessableEntity,
                };
            }
            catch (Exception ex)
            {
                response = new Response<TagResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            return response;
        }
    }
}
