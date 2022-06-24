using Application.Commands;
using Application.Commands.Users;
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
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.Email, act => act.Ignore())
                .ReverseMap();

            CreateMap<CreateUserCommand, User>()
                .ReverseMap();
        }
    }
}
