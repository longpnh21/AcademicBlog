using Application.Mappers;
using Application.Queries.Blogs;
using Application.Response;
using Application.Response.Base;
using Core.Repositories;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Blogs
{
    public class GetBlogWithIdHandler : IRequestHandler<GetBlogWithIdQuery, Response<BlogResponse>>
    {
        private readonly IBlogRepository _blogRepository;

        public GetBlogWithIdHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<Response<BlogResponse>> Handle(GetBlogWithIdQuery query, CancellationToken cancellationToken)
        {
            var response = new Response<BlogResponse>();
            string includedProperties = "Media";
            try
            {
                var result = await _blogRepository.GetByIdAsync(query.Id, includedProperties);
                if (result is null)
                {
                    throw new NullReferenceException("Not found blog");
                }

                var mappedResult = AcademicBlogMapper.Mapper.Map<BlogResponse>(result);
                if (mappedResult is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                response = new Response<BlogResponse>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (NullReferenceException ex)
            {
                response = new Response<BlogResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.NotFound,
                };
            }
            catch (ArgumentException ex)
            {
                response = new Response<BlogResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.UnprocessableEntity,
                };
            }
            catch (Exception ex)
            {
                response = new Response<BlogResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            return response;
        }
    }
}
