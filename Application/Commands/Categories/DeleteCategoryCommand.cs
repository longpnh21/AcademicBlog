using Application.Response;
using Application.Response.Base;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Categories
{
    public class DeleteCategoryCommand : IRequest<Response<CategoryResponse>>
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
    }
}
