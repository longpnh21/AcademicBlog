using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class VoteRepository : BaseRepository<Vote>, IVoteRepository
    {
        public VoteRepository(AcademicBlogContext context) : base(context)
        {
        }
    }
}
