using Application.Commands.Tags;
using Application.Response;
using Application.Response.Base;
using Core.Repositories;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Tags
{
    public class DeleteTagHandler : IRequestHandler<DeleteTagCommand, Response<TagResponse>>
    {
        private readonly ITagRepository _tagRepository;

        public DeleteTagHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Response<TagResponse>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<TagResponse>();
            try
            {
                var result = await _tagRepository.GetByIdAsync(new object[] { request.Id });
                if (result is null)
                {
                    throw new NullReferenceException("Not found tag");
                }
                await _tagRepository.DeleteAsync(result);

                response = new Response<TagResponse>()
                {
                    StatusCode = HttpStatusCode.NoContent
                };
            }
            catch (NullReferenceException ex)
            {
                response = new Response<TagResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.NotFound
                };
            }
            catch (Exception ex)
            {
                response = new Response<TagResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            return response;
        }
    }
}
