using Application.Commands.Tags;
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

namespace Application.Handlers.Tags
{
    public class EditTagHandler : IRequestHandler<EditTagCommand, Response<TagResponse>>
    {
        private readonly ITagRepository _tagRepository;

        public EditTagHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Response<TagResponse>> Handle(EditTagCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<TagResponse>();
            try
            {
                var entity = AcademicBlogMapper.Mapper.Map<Tag>(request);
                if (entity is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                var newTag = await _tagRepository.UpdateAsync(entity);
                response = new Response<TagResponse>()
                {
                    StatusCode = HttpStatusCode.NoContent,
                };

            }
            catch (ApplicationException ex)
            {
                response = new Response<TagResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.UnprocessableEntity
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
