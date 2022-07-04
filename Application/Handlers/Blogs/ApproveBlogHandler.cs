using Application.Commands.Blogs;
using Application.Mappers;
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
    public class ApproveBlogHandler : IRequestHandler<ApproveBlogCommand, Response<BlogResponse>>
    {
        private readonly IBlogRepository _BlogRepository;

        public ApproveBlogHandler(IBlogRepository BlogRepository)
        {
            _BlogRepository = BlogRepository;
        }

        public async Task<Response<BlogResponse>> Handle(ApproveBlogCommand request, CancellationToken cancellationToken)
        {
            var entity = await _BlogRepository.GetByIdAsync(request.BlogId);
            var response = new Response<BlogResponse>();
            try
            {
                if (entity is null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    return response;
                }
                entity.ApproverId = request.ApproverId;
                entity.Status = Core.Enums.BlogStatus.Available;

                var newBlog = await _BlogRepository.UpdateAsync(entity);
                response = new Response<BlogResponse>(AcademicBlogMapper.Mapper.Map<BlogResponse>(newBlog))
                {
                    StatusCode = HttpStatusCode.OK,
                };

            }
            catch (ApplicationException ex)
            {
                response = new Response<BlogResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.UnprocessableEntity
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
