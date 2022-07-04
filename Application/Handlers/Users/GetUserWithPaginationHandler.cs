﻿using Application.Mappers;
using Application.Queries.Users;
using Application.Response;
using Application.Response.Base;
using Core.Common;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Users
{
    public class GetUserWithPaginationHandler : IRequestHandler<GetUserWithPaginationQuery, Response<PaginatedList<UserResponse>>>
    {
        private readonly IUserRepository _UserRepository;

        public GetUserWithPaginationHandler(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;

        }

        public async Task<Response<PaginatedList<UserResponse>>> Handle(GetUserWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<PaginatedList<UserResponse>>();
            try
            {
                if (request.PageIndex <= 0 || request.PageSize <= 0)
                {
                    throw new ArgumentException("Invalid request");
                }

                var result = await _UserRepository.GetWithPaginationAsync(request.PageIndex, request.PageSize);
                var mappedResult = AcademicBlogMapper.Mapper.Map<PaginatedList<User>, PaginatedList<UserResponse>>(result);
                response = new Response<PaginatedList<UserResponse>>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (ArgumentException ex)
            {
                response = new Response<PaginatedList<UserResponse>>(ex.Message)
                {
                    StatusCode = HttpStatusCode.BadRequest,
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
