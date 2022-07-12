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
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, Response<CommentResponse>>
    {
        private readonly ICommentRepository _commentRepository;

        public CreateCommentHandler(ICommentRepository CommentRepository)
        {
            _commentRepository = CommentRepository;
        }

        public async Task<Response<CommentResponse>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<CommentResponse>();
            try
            {
                var entity = AcademicBlogMapper.Mapper.Map<Comment>(request);
                if (entity is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                var newComment = await _commentRepository.AddAsync(entity);
                response = new Response<CommentResponse>(AcademicBlogMapper.Mapper.Map<CommentResponse>(newComment))
                {
                    StatusCode = HttpStatusCode.Created
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
