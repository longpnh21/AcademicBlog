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
    public class CreateBlogHandler : IRequestHandler<CreateBlogCommand, Response<BlogResponse>>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUploadService _uploadService;

        public CreateBlogHandler(IBlogRepository blogRepository, IUploadService uploadService)
        {
            _blogRepository = blogRepository;
            _uploadService = uploadService;
        }

        public async Task<Response<BlogResponse>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<BlogResponse>();
            try
            {
                var entity = AcademicBlogMapper.Mapper.Map<Blog>(request);
                if (entity is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                if (request.Media != null)
                {
                    foreach (var media in request.Media)
                    {
                        string link = await _uploadService.UploadFileAsync(media);
                        if (link is not null)
                        {
                            entity.Media.Add(new Media() { Link = link });
                        }
                    }
                }

                entity.Status = BlogStatus.Pending;

                var newBlog = await _blogRepository.AddAsync(entity);
                response = new Response<BlogResponse>(AcademicBlogMapper.Mapper.Map<BlogResponse>(newBlog))
                {
                    StatusCode = HttpStatusCode.Created
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
