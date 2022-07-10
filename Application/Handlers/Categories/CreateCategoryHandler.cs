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
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Response<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Response<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<CategoryResponse>();
            try
            {
                var entity = AcademicBlogMapper.Mapper.Map<Category>(request);
                if (entity is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                var newCategory = await _categoryRepository.AddAsync(entity);
                response = new Response<CategoryResponse>(AcademicBlogMapper.Mapper.Map<CategoryResponse>(newCategory))
                {
                    StatusCode = HttpStatusCode.Created
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
