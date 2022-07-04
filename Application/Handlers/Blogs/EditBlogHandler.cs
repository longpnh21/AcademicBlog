using Application.Commands.Blogs;
using Application.Interfaces;
using Application.Mappers;
using Application.Response;
using Application.Response.Base;
using Core.Entities;
using Core.Enums;
using Core.Repositories;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Blogs
{
    public class EditBlogHandler : IRequestHandler<EditBlogCommand, Response<BlogResponse>>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUploadService _uploadService;

        public EditBlogHandler(IBlogRepository blogRepository, IUploadService uploadService)
        {
            _blogRepository = blogRepository;
            _uploadService = uploadService;
        }

        public async Task<Response<BlogResponse>> Handle(EditBlogCommand request, CancellationToken cancellationToken)
        {
            var inDatabase = await _blogRepository.GetByIdAsync(request.Id);
            var entity = AcademicBlogMapper.Mapper.Map<Blog>(request);
            var response = new Response<BlogResponse>();
            try
            {
                if (inDatabase is null)
                {
                    throw new ArgumentNullException("Not found entity");
                }
                if (entity is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                entity.Status = BlogStatus.Pending;

                var newBlog = await _blogRepository.UpdateAsync(entity);
                response = new Response<BlogResponse>()
                {
                    StatusCode = HttpStatusCode.NoContent
                };

            }
            catch (ApplicationException ex)
            {
                response = new Response<BlogResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.UnprocessableEntity
                };
            }
            catch (ArgumentNullException ex)
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