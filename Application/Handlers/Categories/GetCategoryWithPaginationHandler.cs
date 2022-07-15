using Application.Mappers;
using Application.Queries.Categories;
using Application.Response;
using Application.Response.Base;
using Core.Common;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Categories
{
    public class GetCategoryWithPaginationHandler : IRequestHandler<GetCategoryWithPaginationQuery, Response<PaginatedList<CategoryResponse>>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryWithPaginationHandler(ICategoryRepository CategoryRepository)
        {
            _categoryRepository = CategoryRepository;
        }

        public async Task<Response<PaginatedList<CategoryResponse>>> Handle(GetCategoryWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<PaginatedList<CategoryResponse>>();
            try
            {
                var filter = new List<Expression<Func<Category, bool>>>();
                if (string.IsNullOrWhiteSpace(request.SearchValue))
                {
                    filter.Add(e => e.Name.Contains(request.SearchValue));
                }
                var result = await _categoryRepository.GetWithPaginationAsync(request.PageIndex, request.PageSize, filter: filter);
                var mappedResult = AcademicBlogMapper.Mapper.Map<PaginatedList<Category>, PaginatedList<CategoryResponse>>(result);

                response = new Response<PaginatedList<CategoryResponse>>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                response = new Response<PaginatedList<CategoryResponse>>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

            return response;

        }
    }
}
