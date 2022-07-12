using Application.Mappers;
using Application.Queries.Comments;
using Application.Response;
using Application.Response.Base;
using Core.Common;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Comments
{
    public class GetCommentWithPaginationHandler : IRequestHandler<GetCommentWithPaginationQuery, Response<PaginatedList<CommentResponse>>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentWithPaginationHandler(ICommentRepository CommentRepository)
        {
            _commentRepository = CommentRepository;
        }

        public async Task<Response<PaginatedList<CommentResponse>>> Handle(GetCommentWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<PaginatedList<CommentResponse>>();
            try
            {
                var result = await _commentRepository.GetWithPaginationAsync(request.PageIndex, request.PageSize, c => c.ReferenceId == null);
                var mappedResult = AcademicBlogMapper.Mapper.Map<PaginatedList<Comment>, PaginatedList<CommentResponse>>(result);

                foreach (CommentResponse comment in mappedResult)
                {
                    var replyResult = await _commentRepository.GetAllReply(comment.Id);
                    comment.Reply = AcademicBlogMapper.Mapper.Map<IEnumerable<Comment>, IEnumerable<ReplyResponse>>(replyResult);
                }
                

                response = new Response<PaginatedList<CommentResponse>>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                response = new Response<PaginatedList<CommentResponse>>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

            return response;

        }
    }
}
