using Application.Commands.Categories;
using Application.Mappers;
using Application.Response;
using Application.Response.Base;
using Core.Entities;
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
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Response<CategoryResponse>>
    {
        private readonly ICategoryRepository _CategoryRepository;

        public DeleteCategoryHandler(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }

        public async Task<Response<CategoryResponse>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<CategoryResponse>();
            try
            {
                var result = await _CategoryRepository.GetByIdAsync(request.CategoryId);

                await _CategoryRepository.DeleteAsync(result);
                response = new Response<CategoryResponse>()
                {
                    StatusCode = HttpStatusCode.OK
                };

            }
            catch (ApplicationException ex)
            {
                response = new Response<CategoryResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.UnprocessableEntity;
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
