using Application.Response;
using Application.Response.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Users
{
    public class EditUserCommand : IRequest<Response<UserResponse>>
    {
        [Required]
        [StringLength(300)]
        public string Id { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        [StringLength(10)]
        public string Role { get; set; }
    }
}
