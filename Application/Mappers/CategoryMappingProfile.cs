using Application.Commands.Categories;
using Application.Response;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryResponse>().ReverseMap();

            CreateMap<CreateCategoryCommand, Category>()
                .ReverseMap();

            CreateMap<EditCategoryCommand, Category>()
                .ReverseMap();

            CreateMap<DeleteCategoryCommand, Category>()
                .ReverseMap();
        }
    }
}
