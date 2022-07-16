using Application.Commands.Votes;
using Application.Response;
using AutoMapper;
using Core.Entities;

namespace Application.Mappers
{
    public class VoteMappingProfile : Profile
    {
        public VoteMappingProfile()
        {
            CreateMap<Vote, VoteResponse>().ReverseMap();
            CreateMap<CreateVoteCommand, Vote>();
            CreateMap<EditVoteCommand, Vote>();
        }
    }
}
