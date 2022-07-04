using Application.Response;
using Application.Response.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Categories
{
    public class CreateCategoryCommand : IRequest<Response<CategoryResponse>>
    {
        [Required]
        public string Name { get; set; }
    }
}
