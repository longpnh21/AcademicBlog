using Application.Commands;
using Application.Commands.Tags;
using Application.Response;
using Application.Response.Base;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
