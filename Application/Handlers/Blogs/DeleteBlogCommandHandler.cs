using Application.Commands.Blogs;
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
    public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand, Response<BlogResponse>>
    {
        private readonly IBlogRepository _blogRepository;

        public DeleteBlogCommandHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<Response<BlogResponse>> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<BlogResponse>();
            try
            {
                var result = await _blogRepository.GetByIdAsync(request.Id);
                if (result is null)
                {
                    throw new NullReferenceException("Not found blog");
                }

                await _blogRepository.DeleteAsync(result);

                response = new Response<BlogResponse>()
                {
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            catch (NullReferenceException ex)
            {
                response = new Response<BlogResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.NotFound
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
