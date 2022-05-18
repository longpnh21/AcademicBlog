using Application.Commands;
using Application.Interfaces;
using Application.Mappers;
using Application.Response.Base;
using Application.Response;
using Core.Entities;
using Core.Enums;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries;
using AutoMapper;
using Core.Common;

namespace Application.Handlers.Blogs
{
    public class GetBlogWithPaginationHandler : IRequestHandler<GetBlogWithPaginationQuery, Response<PaginatedList<BlogResponse>>>
    {
        private readonly IBlogRepository _blogRepository;

        public GetBlogWithPaginationHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;

        }

        public async Task<Response<PaginatedList<BlogResponse>>> Handle(GetBlogWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<PaginatedList<BlogResponse>>();

            try
            {
                if (request.PageIndex <= 0 || request.PageSize <= 0)
                {
                    throw new ArgumentException("Invalid request");
                }

                var result = await _blogRepository.GetWithPaginationAsync(request.PageIndex, request.PageSize);
                var mappedResult = new PaginatedList<BlogResponse>(result.Select(e => AcademicBlogMapper.Mapper.Map<BlogResponse>(e)), request.PageIndex, request.PageSize);
                response = new Response<PaginatedList<BlogResponse>>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (ArgumentException ex)
            {
                response = new Response<PaginatedList<BlogResponse>>(ex.Message)
                {
                    StatusCode = HttpStatusCode.BadRequest,
                };
                
            }
            catch (Exception ex)
            {
                response = new Response<PaginatedList<BlogResponse>>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;

        }
    }
}
