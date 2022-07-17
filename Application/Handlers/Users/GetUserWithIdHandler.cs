using Application.Mappers;
using Application.Queries.Users;
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

namespace Application.Handlers.Tags
{
    public class GetUserWithIdHandler : IRequestHandler<GetUserWithIdQuery, Response<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _roleManager;

        public GetUserWithIdHandler(IUserRepository userRepository, UserManager<User> roleManager)
        {
            _userRepository = userRepository;
            this._roleManager = roleManager;
        }

        public async Task<Response<UserResponse>> Handle(GetUserWithIdQuery query, CancellationToken cancellationToken)
        {
            var response = new Response<UserResponse>();

            try
            {
                var result = await _userRepository.GetByIdAsync(query.Id);
                if (result is null)
                {
                    throw new NullReferenceException("Not found user");
                }
                var mappedResult = AcademicBlogMapper.Mapper.Map<UserResponse>(result);
                if (mappedResult is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                mappedResult.Role = (await _roleManager.GetRolesAsync(result)).FirstOrDefault();

                response = new Response<UserResponse>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (NullReferenceException ex)
            {
                response = new Response<UserResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
            catch (ApplicationException ex)
            {
                response = new Response<UserResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.UnprocessableEntity,
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
