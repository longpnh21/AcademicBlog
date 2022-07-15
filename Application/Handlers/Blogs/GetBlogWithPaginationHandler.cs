using Application.Mappers;
using Application.Queries.Blogs;
using Application.Response;
using Application.Response.Base;
using Core.Common;
using Core.Entities;
using Core.Enums;
using Core.Repositories;
using Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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
            List<Expression<Func<Category, bool>>> filter = new();
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null;
            string includedProperties = "Media";
            try
            {
                if (!string.IsNullOrWhiteSpace(request.UserId))
                {
                    filter.Add(e => e.CreatorId == request.UserId);
                }
                if (!request.IsStatusVisable)
                {
                    filter.Add(e => e.Status == BlogStatus.Available);
                }
                if (string.IsNullOrWhiteSpace(request.OrderBy))
                {
                    if (request.IsStatusVisable)
                    {
                        orderBy = e => e.OrderByDescending(x => x.ModifiedTime);
                    }
                    else
                    {
                        orderBy = e => e.OrderBy(x => x.ModifiedTime);
                    }
                }
                var result = await _blogRepository.SearchAsync(request.SearchValue, request.PageIndex, request.PageSize, filter: filter, orderBy: orderBy,includeProperties: includedProperties);
                var mappedResult = AcademicBlogMapper.Mapper.Map<PaginatedList<Category>, PaginatedList<BlogResponse>>(result);

                response = new Response<PaginatedList<BlogResponse>>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                response = new Response<PaginatedList<BlogResponse>>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            return response;
        }
    }
}
