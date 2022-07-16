using Application.Commands.Comments;
using Application.Response;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers
{
    public class CommentMapperProfile : Profile
    {
        public CommentMapperProfile()
        {
            CreateMap<Comment, CommentResponse>().ReverseMap();

            CreateMap<Comment, ReplyResponse>().ReverseMap();

            CreateMap<CreateCommentCommand, Comment>();

            CreateMap<EditCommentCommand, Comment>();
        }
    }
}
