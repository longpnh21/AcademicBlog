using Application.Response;
using Application.Response.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Comments
{
    public class DeleteCommentCommand: IRequest<Response<CommentResponse>>
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
    }
}
