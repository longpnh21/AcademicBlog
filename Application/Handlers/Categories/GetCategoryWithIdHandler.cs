using Application.Commands.Categories;
using Application.Mappers;
using Application.Queries.Categories;
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

namespace Application.Handlers.Categories
{
    public class GetCategoryWithIdHandler : IRequestHandler<GetCategoryWithIdQuery, Response<CategoryResponse>>
    {
        private readonly ICategoryRepository _CategoryRepository;

        public GetCategoryWithIdHandler(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;

        }

        public async Task<Response<CategoryResponse>> Handle(GetCategoryWithIdQuery query, CancellationToken cancellationToken)
        {
            var response = new Response<CategoryResponse>();

            try
            {
                var result = await _CategoryRepository.GetByIdAsync(query.CategoryId);
                var mappedResult = new CategoryResponse();
                mappedResult = AcademicBlogMapper.Mapper.Map<CategoryResponse>(result);
                response = new Response<CategoryResponse>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (ArgumentException ex)
            {
                response = new Response<CategoryResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.BadRequest,
                };

            }
            catch (Exception ex)
            {
                response = new Response<CategoryResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;

        }
    }
}
