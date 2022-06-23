using Application.Response;
using Application.Response.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Categories
{
    public class DeleteCategoryCommand : IRequest<Response<CategoryResponse>>
    {
        public int CategoryId { get; set; }
    }
}
