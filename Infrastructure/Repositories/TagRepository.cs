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
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(AcademicBlogContext context) : base(context) { }

    }
}
