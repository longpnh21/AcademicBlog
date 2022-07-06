using Application.Response;
using Application.Response.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users
{
    public class GetUserWithIdQuery : IRequest<Response<UserResponse>>
    {
        [Required]
        [Range(0, int.MaxValue)]
        public string Id { get; set; }
    }
}
