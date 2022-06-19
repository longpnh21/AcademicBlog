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
    public class EditTagHandler : IRequestHandler<EditTagCommand, Response<TagResponse>>
    {
        private readonly ITagRepository _tagRepository;

        public EditTagHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<Response<TagResponse>> Handle(EditTagCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine(request);
            var entity = AcademicBlogMapper.Mapper.Map<Tag>(request);
            var response = new Response<TagResponse>();
            Console.WriteLine(entity);
            try
            {
                if (entity is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }
                Console.WriteLine(entity);

                var newTag = await _tagRepository.UpdateAsync(entity);
                response = new Response<TagResponse>(AcademicBlogMapper.Mapper.Map<TagResponse>(newTag))
                {
                    StatusCode = HttpStatusCode.OK,
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
