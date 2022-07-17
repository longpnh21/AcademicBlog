﻿using Application.Mappers;
using Application.Queries.Users;
using Application.Response;
using Application.Response.Base;
using Core.Common;
using Core.Entities;
using Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Users
{
    public class GetUserWithPaginationHandler : IRequestHandler<GetUserWithPaginationQuery, Response<PaginatedList<UserResponse>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public GetUserWithPaginationHandler(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            this._userManager = userManager;
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
                var result = await _userRepository.GetWithPaginationAsync(request.PageIndex, request.PageSize, filter: filter, isDelete: request.IsDeleted);
                var mappedResult = AcademicBlogMapper.Mapper.Map<PaginatedList<User>, PaginatedList<UserResponse>>(result);

                foreach (var user in mappedResult)
                {
                    user.Role = (await _userManager.GetRolesAsync(AcademicBlogMapper.Mapper.Map<User>(user))).FirstOrDefault();
                }

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
