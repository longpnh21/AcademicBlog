using Application.Mappers;
using Application.Queries.Blogs;
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

namespace Application.Handlers.Blogs
{
    public class GetBlogWithPaginationHandler : IRequestHandler<GetBlogWithPaginationQuery, Response<PaginatedList<BlogResponse>>>
    {
        private readonly IBlogRepository _blogRepository;

        public GetBlogWithPaginationHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;

        }

        public async Task<Response<PaginatedList<BlogResponse>>> Handle(GetBlogWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<PaginatedList<BlogResponse>>();

            try
            {
                if (request.PageIndex <= 0 || request.PageSize <= 0)
                {
                    throw new ArgumentException("Invalid request");
                }

                var result = await _blogRepository.GetWithPaginationAsync(request.PageIndex, request.PageSize);
                var mappedResult = AcademicBlogMapper.Mapper.Map<PaginatedList<Blog>, PaginatedList<BlogResponse>>(result);
                response = new Response<PaginatedList<BlogResponse>>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (ArgumentException ex)
            {
                response = new Response<PaginatedList<BlogResponse>>(ex.Message)
                {
                    StatusCode = HttpStatusCode.BadRequest,
                };

            }
            catch (Exception ex)
            {
                response = new Response<PaginatedList<BlogResponse>>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

            return response;

        }
    }
}
