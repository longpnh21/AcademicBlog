using Application.Mappers;
using Application.Queries.Categories;
using Application.Response;
using Application.Response.Base;
using Core.Repositories;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Categories
{
    public class GetCategoryWithIdHandler : IRequestHandler<GetCategoryWithIdQuery, Response<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryWithIdHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Response<CategoryResponse>> Handle(GetCategoryWithIdQuery query, CancellationToken cancellationToken)
        {
            var response = new Response<CategoryResponse>();
            try
            {
                var result = await _categoryRepository.GetByIdAsync(new object[] { query.Id });
                if (result is null)
                {
                    throw new NullReferenceException("Not found category");
                }

                var mappedResult = AcademicBlogMapper.Mapper.Map<CategoryResponse>(result);
                if (mappedResult is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }
                response = new Response<CategoryResponse>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (NullReferenceException ex)
            {
                response = new Response<CategoryResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.NotFound,
                };
            }
            catch (ApplicationException ex)
            {
                response = new Response<CategoryResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.UnprocessableEntity,
                };

            }
            catch (Exception ex)
            {
                response = new Response<CategoryResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

            return response;

        }
    }
}
