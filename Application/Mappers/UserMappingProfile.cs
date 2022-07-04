using Application.Commands.Users;
using Application.Response;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserResponse>()
                .ReverseMap();

            CreateMap<CreateUserCommand, User>()
                .ReverseMap();
        }
    }
}
