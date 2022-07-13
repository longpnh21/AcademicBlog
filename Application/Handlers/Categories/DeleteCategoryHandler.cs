using Application.Commands.Categories;
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
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Response<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryHandler(ICategoryRepository CategoryRepository)
        {
            _categoryRepository = CategoryRepository;
        }

        public async Task<Response<CategoryResponse>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<CategoryResponse>();
            try
            {
                var result = await _categoryRepository.GetByIdAsync(new object[] { request.Id });
                if (result is null)
                {
                    throw new NullReferenceException();
                }

                await _categoryRepository.DeleteAsync(result);
                response = new Response<CategoryResponse>()
                {
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            catch (NullReferenceException ex)
            {
                response = new Response<CategoryResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.NotFound
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
