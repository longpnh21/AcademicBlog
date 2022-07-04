using Application.Response;
using Application.Response.Base;
using MediatR;

namespace Application.Queries.Categories
{
    public class GetCategoryWithIdQuery : IRequest<Response<CategoryResponse>>
    {
        public int CategoryId { get; set; }
    }
}
