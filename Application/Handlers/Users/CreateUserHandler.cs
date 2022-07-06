using Application.Commands.Users;
using Application.Mappers;
using Application.Response;
using Application.Response.Base;
using Core.Entities;
using Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Categories
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Response<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public CreateUserHandler(IUserRepository UserRepository, UserManager<User> UserManager)
        {
            _userRepository = UserRepository;
            _userManager = UserManager;
        }

        public async Task<Response<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<UserResponse>();
            try
            {
                if (request.Role != "Student" && request.Role != "Mentor")
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                }

                var User = new User() { UserName = request.Email, Email = request.Email, FullName = request.FullName };
                var UserRole = new IdentityRole(request.Role);

                if (_userManager.Users.All(u => u.UserName != User.UserName))
                {
                    var result = await _userManager.CreateAsync(User, request.Password);
                    await _userManager.AddToRolesAsync(User, new[] { UserRole.Name });
                }

                response = new Response<UserResponse>(AcademicBlogMapper.Mapper.Map<UserResponse>(User));

            }
            catch (Exception ex)
            {
                response = new Response<UserResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            return response;
        }
    }
}
