using Application.Mappers;
using Application.Queries.Blogs;
using Application.Response;
using Application.Response.Base;
using Core.Common;
using Core.Entities;
using Core.Enums;
using Core.Repositories;
using MediatR;
using System;
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
            Expression<Func<Blog, bool>> filter = null;
            Func<IQueryable<Blog>, IOrderedQueryable<Blog>> orderBy = null;
            string includedProperties = "Media";
            try
            {
                if (!string.IsNullOrWhiteSpace(request.UserId))
                {
                    filter = e => e.CreatorId == request.UserId;
                }
                if (!request.IsApprover)
                {
                    filter = e => e.Status == BlogStatus.Available;
                }
                var result = await _blogRepository.GetWithPaginationAsync(request.PageIndex, request.PageSize, filter: filter, orderBy: orderBy,includeProperties: includedProperties);
                var mappedResult = AcademicBlogMapper.Mapper.Map<PaginatedList<Blog>, PaginatedList<BlogResponse>>(result);

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
