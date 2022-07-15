using Application.Commands.Blogs;
using Application.Response;
using Application.Response.Base;
using Core.Entities;
using Core.Enums;
using Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Blogs
{
    public class ApproveBlogHandler : IRequestHandler<ApproveBlogCommand, Response<BlogResponse>>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly UserManager<User> _userManager;

        public ApproveBlogHandler(IBlogRepository blogRepository, UserManager<User> userManager)
        {
            _blogRepository = blogRepository;
            _userManager = userManager;
        }

        public async Task<Response<BlogResponse>> Handle(ApproveBlogCommand request, CancellationToken cancellationToken)
        {

            var response = new Response<BlogResponse>();
            try
            {
                var entity = await _blogRepository.GetByIdAsync(request.Id);
                if (entity is null)
                {
                    throw new NullReferenceException("Not found blog");
                }

                var approver = await _userManager.FindByIdAsync(request.ApproverId);
                if (approver is null)
                {
                    throw new NullReferenceException("Not found approver");
                }

                entity.ApproverId = approver.Id;
                entity.Status = request.Status;

                await _blogRepository.UpdateAsync(entity);

                response = new Response<BlogResponse>()
                {
                    StatusCode = HttpStatusCode.NoContent,
                };
            }
            catch (NullReferenceException ex)
            {
                response = new Response<BlogResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.NotFound
                };
            }
            catch (Exception ex)
            {
                response = new Response<BlogResponse>(ex.Message)
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
            return response;
        }
    }
}
