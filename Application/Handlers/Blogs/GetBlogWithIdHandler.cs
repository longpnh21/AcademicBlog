using Application.Commands.Blogs;
using Application.Mappers;
using Application.Queries;
using Application.Queries.Blogs;
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

            try
            {
                var result = await _blogRepository.GetByIdAsync(query.BlogId);
                var mappedResult = new BlogResponse();
                mappedResult = AcademicBlogMapper.Mapper.Map<BlogResponse>(result);
                response = new Response<BlogResponse>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (ArgumentException ex)
            {
                response = new Response<BlogResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.BadRequest,
                };

            }
            catch (Exception ex)
            {
                response = new Response<BlogResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;

        }
    }
}
