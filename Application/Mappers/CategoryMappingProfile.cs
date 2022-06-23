using Application.Commands.Categories;
using Application.Response;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
