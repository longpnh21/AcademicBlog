using Application.Response;
using Application.Response.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Users
{
    public class CreateUserCommand : IRequest<Response<UserResponse>>
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }

    }
}
