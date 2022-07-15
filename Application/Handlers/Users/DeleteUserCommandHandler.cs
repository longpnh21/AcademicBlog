using Application.Commands.Users;
using Application.Response;
using Application.Response.Base;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Users
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<UserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<UserResponse>();
            try
            {
                var result = await _userRepository.GetByIdAsync(request.Id);
                if (result is null)
                {
                    throw new NullReferenceException("Not found user");
                }
                await _userRepository.DeleteAsync(result);

                response = new Response<UserResponse>()
                {
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            catch (NullReferenceException ex)
            {
                response = new Response<UserResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.NotFound
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
