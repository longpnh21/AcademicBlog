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

namespace Application.Handlers.Users
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, Response<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public EditUserCommandHandler(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            this._userManager = userManager;
        }

        public async Task<Response<UserResponse>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<UserResponse>();

            try
            {
                var user = await _userRepository.GetByIdAsync(request.Id);
                if (user is null)
                {
                    throw new NullReferenceException("Not found user");
                }

                var role = await _userManager.GetRolesAsync(user);

                user.FullName = request.FullName;
                await _userRepository.UpdateAsync(user);

                await _userManager.RemoveFromRoleAsync(user, role[0]);
                await _userManager.AddToRoleAsync(user, request.Role);

                response = new Response<UserResponse>()
                {
                    StatusCode = HttpStatusCode.NoContent,
                };
            }
            catch (NullReferenceException ex)
            {
                response = new Response<UserResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.BadRequest,
                };
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

