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
    public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, Response<UserResponse>>
    {
        private readonly IUserRepository _UserRepository;
        private UserManager<User> _UserManager;

        public CreateStudentHandler(IUserRepository UserRepository, UserManager<User> UserManager)
        {
            _UserRepository = UserRepository;
            _UserManager = UserManager;
        }

        public async Task<Response<UserResponse>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<UserResponse>();
            try
            {
                var student = new User() { UserName = request.Email, Email = request.Email, FullName = request.FullName };
                var studentRole = new IdentityRole("Student");

                if (_UserManager.Users.All(u => u.UserName != student.UserName))
                {
                    var result = await _UserManager.CreateAsync(student, request.Password);
                    await _UserManager.AddToRolesAsync(student, new[] { studentRole.Name });
                }
                response = new Response<UserResponse>(AcademicBlogMapper.Mapper.Map<UserResponse>(student));

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
