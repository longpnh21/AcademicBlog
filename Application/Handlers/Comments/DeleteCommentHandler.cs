using Application.Commands.Comments;
using Application.Response;
using Application.Response.Base;
using Core.Repositories;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Comments
{
    public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, Response<CommentResponse>>
    {
        private readonly ICommentRepository _commentRepository;

        public DeleteCommentHandler(ICommentRepository CommentRepository)
        {
            _commentRepository = CommentRepository;
        }

        public async Task<Response<CommentResponse>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<CommentResponse>();
            try
            {
                var result = await _commentRepository.GetByIdAsync(request.Id);
                if (result is null)
                {
                    throw new NullReferenceException("Not found comment");
                }
                await _commentRepository.DeleteAsync(result);

                response = new Response<CommentResponse>()
                {
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            catch (NullReferenceException ex)
            {
                response = new Response<CommentResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.NotFound
                };
            }
            catch (Exception ex)
            {
                response = new Response<CommentResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            return response;
        }
    }
}
