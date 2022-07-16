using Application.Commands.Users;
using Application.Mappers;
using Application.Response;
using Application.Response.Base;
using Core.Entities;
using Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Users
{
    public class ChangeUserStatusHandler : IRequestHandler<ChangeUserStatusCommand, Response<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public ChangeUserStatusHandler(IUserRepository UserRepository, UserManager<User> UserManager)
        {
            _userRepository = UserRepository;
            _userManager = UserManager;
        }

        public async Task<Response<UserResponse>> Handle(ChangeUserStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<UserResponse>();
            try
            {
                var result = await _userRepository.GetByIdAsync(request.Id,true);
                if (result is null)
                {
                    throw new NullReferenceException("Not found user");
                }

                await _userRepository.UpdateUserStatus(result);
                response = new Response<UserResponse>()
                {
                    StatusCode = HttpStatusCode.NoContent,
                };

            }
            catch (ApplicationException ex)
            {
                response = new Response<UserResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.UnprocessableEntity
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
