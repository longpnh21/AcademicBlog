using Application.Commands.Categories;
using Application.Mappers;
using Application.Response;
using Application.Response.Base;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Categories
{
    public class EditCategoryHandler : IRequestHandler<EditCategoryCommand, Response<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public EditCategoryHandler(ICategoryRepository CategoryRepository)
        {
            _categoryRepository = CategoryRepository;
        }

        public async Task<Response<CategoryResponse>> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<CategoryResponse>();
            try
            {
                var inDatabase = await _categoryRepository.GetByIdAsync(new object[] { request.Id });
                if (inDatabase is null)
                {
                    throw new ApplicationException("Not found category");
                }

                var entity = AcademicBlogMapper.Mapper.Map<Category>(request);
                if (entity is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                await _categoryRepository.UpdateAsync(entity);

                response = new Response<CategoryResponse>()
                {
                    StatusCode = HttpStatusCode.NoContent,
                };

            }
            catch (ApplicationException ex)
            {
                response = new Response<CategoryResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.UnprocessableEntity
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
