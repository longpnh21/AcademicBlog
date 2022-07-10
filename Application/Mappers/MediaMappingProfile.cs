using Application.Response;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers
{
    public class MediaMappingProfile : Profile
    {
        public MediaMappingProfile()
        {
            CreateMap<Media, MediaResponse>().ReverseMap();
        }
    }
}
