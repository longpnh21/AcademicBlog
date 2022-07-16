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
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, Response<BlogResponse>>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IUploadService _uploadService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;

        public CreateBlogCommandHandler(IBlogRepository blogRepository, IUploadService uploadService, ICategoryRepository categoryRepository, ITagRepository tagRepository)
        {
            _blogRepository = blogRepository;
            _uploadService = uploadService;
            this._categoryRepository = categoryRepository;
            this._tagRepository = tagRepository;
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

                if (request.Categories != null && request.Categories.Count > 0)
                {
                    foreach (var categoryId in request.Categories)
                    {
                        var inDatabase = await _categoryRepository.GetByIdAsync(new object[] { categoryId });
                        if (inDatabase is null)
                        {
                            throw new ArgumentNullException($"Not found categoryId: {categoryId}");
                        }
                        entity.BlogCategories.Add(new BlogCategory
                        {
                            Blog = entity,
                            CategoryId = categoryId
                        });
                    }
                }

                if (request.Tags != null && request.Tags.Count > 0)
                {
                    foreach (var tagId in request.Tags)
                    {
                        var inDatabase = await _tagRepository.GetByIdAsync(new object[] { tagId });
                        if (inDatabase is null)
                        {
                            throw new ArgumentNullException($"Not found categoryId: {tagId}");
                        }
                        entity.BlogTags.Add(new BlogTag
                        {
                            Blog = entity,
                            TagId = tagId
                        });
                    }
                }


                entity.Status = BlogStatus.Pending;

                var newBlog = await _blogRepository.AddAsync(entity);
                response = new Response<BlogResponse>(AcademicBlogMapper.Mapper.Map<BlogResponse>(newBlog))
                {
                    StatusCode = HttpStatusCode.Created
                };

            }
            catch (ArgumentNullException ex)
            {
                response = new Response<BlogResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.NotFound
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
