using Application.Response;
using Application.Response.Base;
using MediatR;

namespace Application.Queries.Tags
{
    public class GetTagWithIdQuery : IRequest<Response<TagResponse>>
    {
        public int TagId { get; set; }
    }
}
