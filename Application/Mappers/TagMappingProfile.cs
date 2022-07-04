using Application.Commands.Tags;
using Application.Response;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers
{
    public class TagMappingProfile : Profile
    {
        public TagMappingProfile()
        {
            CreateMap<Tag, TagResponse>().ReverseMap();

            CreateMap<CreateTagCommand, Tag>()
                .ReverseMap();

            CreateMap<EditTagCommand, Tag>()
                .ReverseMap();

            CreateMap<DeleteTagCommand, Tag>()
                .ReverseMap();
        }
    }
}
