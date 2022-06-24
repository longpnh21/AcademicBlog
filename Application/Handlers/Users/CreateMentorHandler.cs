using Application.Commands.Users;
using Application.Mappers;
using Application.Response;
using Application.Response.Base;
using Core.Entities;
using Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Categories
{
    public class CreateMentorHandler : IRequestHandler<CreateMentorCommand, Response<UserResponse>>
    {
        private readonly IUserRepository _UserRepository;
        private UserManager<User> _UserManager;

        public CreateMentorHandler(IUserRepository UserRepository, UserManager<User> UserManager)
        {
            _UserRepository = UserRepository;
            _UserManager = UserManager;
        }

        public async Task<Response<UserResponse>> Handle(CreateMentorCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<UserResponse>();
            try
            {
                var Mentor = new User() { UserName = request.Email, Email = request.Email, FullName = request.FullName };
                var MentorRole = new IdentityRole("Mentor");

                if (_UserManager.Users.All(u => u.UserName != Mentor.UserName))
                {
                    var result = await _UserManager.CreateAsync(Mentor, request.Password);
                    await _UserManager.AddToRolesAsync(Mentor, new[] { MentorRole.Name });
                }
                response = new Response<UserResponse>(AcademicBlogMapper.Mapper.Map<UserResponse>(Mentor));

            }
            catch (ApplicationException ex)
            {
                response = new Response<UserResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.UnprocessableEntity;
            }
            catch (Exception ex)
            {
                response = new Response<UserResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;

        }
    }
}
