﻿using Application.Commands.Tags;
using Application.Mappers;
using Application.Response;
using Application.Response.Base;
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
    public class GetTagWithIdHandler : IRequestHandler<GetTagWithIdCommand, Response<TagResponse>>
    {
        private readonly ITagRepository _tagRepository;

        public GetTagWithIdHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;

        }

        public async Task<Response<TagResponse>> Handle(GetTagWithIdCommand command, CancellationToken cancellationToken)
        {
            var response = new Response<TagResponse>();

            try
            {
                var result = await _tagRepository.GetByIdAsync(command.TagId);
                var mappedResult = new TagResponse();
                mappedResult = AcademicBlogMapper.Mapper.Map<TagResponse>(result);
                response = new Response<TagResponse>(mappedResult)
                {
                    StatusCode = HttpStatusCode.OK,
                };
            }
            catch (ArgumentException ex)
            {
                response = new Response<TagResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.BadRequest,
                };

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
