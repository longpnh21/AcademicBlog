using Application.Response;
using Application.Response.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Users
{
    public class DeleteUserCommand : IRequest<Response<UserResponse>>
    {
        [Required]
        public string Id { get; set; }
    }
}
