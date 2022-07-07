using Application.Mappers;
using Application.Queries.Users;
using Application.Response;
using Application.Response.Base;
using Core.Repositories;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Tags
{
    public class GetUserWithIdHandler : IRequestHandler<GetUserWithIdQuery, Response<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserWithIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;

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
