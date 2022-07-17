using Application.Commands.Blogs;
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
    public class EditBlogCommandHandler : IRequestHandler<EditBlogCommand, Response<BlogResponse>>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly IBlogTagRepository _blogTagRepository;

        public EditBlogCommandHandler(IBlogRepository blogRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository, IBlogCategoryRepository blogCategoryRepository, IBlogTagRepository blogTagRepository)
        {
            _blogRepository = blogRepository;
            this._categoryRepository = categoryRepository;
            this._tagRepository = tagRepository;
            this._blogCategoryRepository = blogCategoryRepository;
            this._blogTagRepository = blogTagRepository;
        }

        public async Task<Response<BlogResponse>> Handle(EditBlogCommand request, CancellationToken cancellationToken)
        {
            var inDatabase = await _blogRepository.GetByIdAsync(request.Id, includeProperties: "BlogCategories,BlogTags");
            var entity = AcademicBlogMapper.Mapper.Map<Blog>(request);
            var response = new Response<BlogResponse>();
            try
            {
                if (inDatabase is null)
                {
                    throw new NullReferenceException("Not found blog");
                }
                if (entity is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                await _blogCategoryRepository.DeleteAsync(inDatabase.BlogCategories);
                await _blogTagRepository.DeleteAsync(inDatabase.BlogTags);

                if (request.Categories != null && request.Categories.Count > 0)
                {
                    foreach (var categoryId in request.Categories)
                    {
                        var inDatabaseCategory = await _categoryRepository.GetByIdAsync(new object[] { categoryId });
                        if (inDatabaseCategory is null)
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
                        var inDatabaseTag = await _tagRepository.GetByIdAsync(new object[] { tagId });
                        if (inDatabaseTag is null)
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
                await _blogRepository.UpdateAsync(entity);
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