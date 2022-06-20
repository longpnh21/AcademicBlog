using Application.Commands.Tags;
using Application.Mappers;
using Application.Response;
using Application.Response.Base;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
                var result = await _tagRepository.GetByIdAsync(request.TagId);

                await _tagRepository.DeleteAsync(result);
                response = new Response<TagResponse>() {
                    StatusCode = HttpStatusCode.OK
                };

            }
            catch (ApplicationException ex)
            {
                response = new Response<TagResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.UnprocessableEntity;
            }
            catch (Exception ex)
            {
                response = new Response<TagResponse>(ex.Message);
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;

        }
    }
}
