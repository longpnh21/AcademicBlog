using Application.Mappers;
using Application.Queries.Categories;
using Application.Response;
using Application.Response.Base;
using Core.Common;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Categories
{
    public class GetCategoryWithPaginationHandler : IRequestHandler<GetCategoryWithPaginationQuery, Response<PaginatedList<CategoryResponse>>>
    {
        private readonly ICategoryRepository _CategoryRepository;

        public GetCategoryWithPaginationHandler(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;

        }

        public async Task<Response<PaginatedList<CategoryResponse>>> Handle(GetCategoryWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<PaginatedList<CategoryResponse>>();

            try
            {
                if (request.PageIndex <= 0 || request.PageSize <= 0)
                {
                    throw new ArgumentException("Invalid request");
                }
                var result = await _CategoryRepository.GetWithPaginationAsync(request.PageIndex, request.PageSize);
                var mappedResult = new PaginatedList<CategoryResponse>(result.Select(e => AcademicBlogMapper.Mapper.Map<CategoryResponse>(e)), request.PageIndex, request.PageSize);
                response = new Response<PaginatedList<CategoryResponse>>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (ArgumentException ex)
            {
                response = new Response<PaginatedList<CategoryResponse>>(ex.Message)
                {
                    StatusCode = HttpStatusCode.BadRequest,
                };

            }
            catch (Exception ex)
            {
                response = new Response<PaginatedList<CategoryResponse>>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;

        }
    }
}
