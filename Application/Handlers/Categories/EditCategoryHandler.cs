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
    public class EditCategoryHandler : IRequestHandler<EditCategoryCommand, Response<CategoryResponse>>
    {
        private readonly ICategoryRepository _CategoryRepository;

        public EditCategoryHandler(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }

        public async Task<Response<CategoryResponse>> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = AcademicBlogMapper.Mapper.Map<Category>(request);
            var response = new Response<CategoryResponse>();
            try
            {
                if (entity is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }
                Console.WriteLine(entity);

                var newCategory = await _CategoryRepository.UpdateAsync(entity);
                response = new Response<CategoryResponse>(AcademicBlogMapper.Mapper.Map<CategoryResponse>(newCategory))
                {
                    StatusCode = HttpStatusCode.OK,
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
