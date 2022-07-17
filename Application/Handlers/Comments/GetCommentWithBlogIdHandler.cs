using Application.Mappers;
using Application.Queries.Comments;
using Application.Response;
using Application.Response.Base;
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
    public class GetCommentWithBlogIdHandler : IRequestHandler<GetCommentWithBlogIdQuery, Response<IEnumerable<CommentResponse>>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentWithBlogIdHandler(ICommentRepository CommentRepository)
        {
            _commentRepository = CommentRepository;
        }

        public async Task<Response<IEnumerable<CommentResponse>>> Handle(GetCommentWithBlogIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<IEnumerable<CommentResponse>>();
            try
            {
                var result = await _commentRepository.GetAllCommentFromPost(request.Id);
                var mappedResult = AcademicBlogMapper.Mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResponse>>(result);

                foreach (CommentResponse comment in mappedResult)
                {
                    var replyResult = await _commentRepository.GetAllReply(comment.Id);
                    comment.Reply = AcademicBlogMapper.Mapper.Map<IEnumerable<Comment>, IEnumerable<ReplyResponse>>(replyResult);
                }


                response = new Response<IEnumerable<CommentResponse>>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                response = new Response<IEnumerable<CommentResponse>>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

            return response;

        }
    }
}
