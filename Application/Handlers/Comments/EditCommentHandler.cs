using Application.Commands.Comments;
using Application.Mappers;
using Application.Response;
using Application.Response.Base;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Comments
{
    public class EditCommentHandler : IRequestHandler<EditCommentCommand, Response<CommentResponse>>
    {
        private readonly ICommentRepository _commentRepository;

        public EditCommentHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Response<CommentResponse>> Handle(EditCommentCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<CommentResponse>();
            try
            {
                var entity = AcademicBlogMapper.Mapper.Map<Comment>(request);
                if (entity is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                var newComment = await _commentRepository.UpdateAsync(entity);
                response = new Response<CommentResponse>()
                {
                    StatusCode = HttpStatusCode.NoContent,
                };

            }
            catch (ApplicationException ex)
            {
                response = new Response<CommentResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.UnprocessableEntity
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
