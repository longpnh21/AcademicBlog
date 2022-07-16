using Application.Mappers;
using Application.Queries.Users;
using Application.Response;
using Application.Response.Base;
using Core.Common;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Users
{
    public class GetUserWithPaginationHandler : IRequestHandler<GetUserWithPaginationQuery, Response<PaginatedList<UserResponse>>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserWithPaginationHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<PaginatedList<UserResponse>>> Handle(GetUserWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<PaginatedList<UserResponse>>();
            try
            {
                var filter = new List<Expression<Func<User, bool>>>();
                if (!string.IsNullOrWhiteSpace(request.SearchValue))
                {
                    filter.Add(e => e.Email.Contains(request.SearchValue) || e.FullName.Contains(request.SearchValue));
                }
                var result = await _userRepository.GetWithPaginationAsync(request.PageIndex, request.PageSize, filter: filter ,isDelete: request.IsDeleted);
                var mappedResult = AcademicBlogMapper.Mapper.Map<PaginatedList<User>, PaginatedList<UserResponse>>(result);

                response = new Response<PaginatedList<UserResponse>>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                response = new Response<PaginatedList<UserResponse>>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            return response;
        }
    }
}
