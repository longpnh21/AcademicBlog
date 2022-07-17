using Application.Response;
using Application.Response.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Queries.Users
{
    public class GetUserWithIdQuery : IRequest<Response<UserResponse>>
    {
        [Required]
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
