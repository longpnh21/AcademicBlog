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
            CreateMap<Blog, BlogResponse>().ReverseMap();

            CreateMap<CreateBlogCommand, Blog>()
                .ForMember(entity => entity.Media, opt => opt.Ignore());

            CreateMap<EditBlogCommand, Blog>()
                .ForMember(entity => entity.Media, opt => opt.Ignore());
        }
    }
}
