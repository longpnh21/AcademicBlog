using Application.Response;
using Application.Response.Base;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Queries.Users
{
    public class GetUserWithIdQuery : IRequest<Response<UserResponse>>
    {
        [Required]
        [Range(0, int.MaxValue)]
        public string Id { get; set; }
    }
}
