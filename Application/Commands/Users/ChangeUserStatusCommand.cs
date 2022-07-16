using Application.Response;
using Application.Response.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Users
{
    public class ChangeUserStatusCommand : IRequest<Response<UserResponse>>
    {
        [Required]
        public string Id { get; set; }
    }
}
