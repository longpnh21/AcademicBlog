using Application.Commands.Votes;
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
    public class VoteMappingProfile : Profile
    {
        public VoteMappingProfile()
        {
            CreateMap<Vote, VoteResponse>().ReverseMap();
            CreateMap<CreateVoteCommand, Vote>();
        }
    }
}
