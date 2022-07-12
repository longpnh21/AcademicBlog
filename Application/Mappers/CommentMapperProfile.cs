using Application.Commands.Comments;
using Application.Response;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class CommentMapperProfile: Profile
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
