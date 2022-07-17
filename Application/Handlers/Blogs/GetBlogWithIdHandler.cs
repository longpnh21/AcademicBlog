using Application.Mappers;
using Application.Queries.Blogs;
using Application.Response;
using Application.Response.Base;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Blogs
{
    public class GetBlogWithIdHandler : IRequestHandler<GetBlogWithIdQuery, Response<BlogResponse>>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;

        public GetBlogWithIdHandler(IBlogRepository blogRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository)
        {
            _blogRepository = blogRepository;
            this._categoryRepository = categoryRepository;
            this._tagRepository = tagRepository;
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

                var categoriesFilter = new List<Expression<Func<Category, bool>>>();
                categoriesFilter.Add(e => e.BlogCategories.Any(x => x.BlogId == mappedResult.Id));

                var categories = await _categoryRepository.GetAllAsync(filter: categoriesFilter);
                mappedResult.Categories = AcademicBlogMapper.Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResponse>>(categories);

                var tagFilter = new List<Expression<Func<Tag, bool>>>();
                tagFilter.Add(e => e.BlogTags.Any(x => x.BlogId == mappedResult.Id));

                var tags = await _tagRepository.GetAllAsync(filter: tagFilter);
                mappedResult.Tags = AcademicBlogMapper.Mapper.Map<IEnumerable<Tag>, IEnumerable<TagResponse>>(tags);

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
