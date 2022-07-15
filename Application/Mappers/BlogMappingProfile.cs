using Application.Commands.Blogs;
using Application.Response;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers
{
    public class BlogMappingProfile : Profile
    {
        public BlogMappingProfile()
        {
            CreateMap<Category, BlogResponse>().ReverseMap();

            CreateMap<CreateBlogCommand, Category>()
                .ForMember(entity => entity.Media, opt => opt.Ignore());

            CreateMap<EditBlogCommand, Category>()
                .ForMember(entity => entity.Media, opt => opt.Ignore());
        }
    }
}
