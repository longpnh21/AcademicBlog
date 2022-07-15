using Application.Response;
using Application.Response.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users
{
    public class DeleteUserCommand : IRequest<Response<UserResponse>>
    {
        [Required]
        public string Id { get; set; }
    }
}
