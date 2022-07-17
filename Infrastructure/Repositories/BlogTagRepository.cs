using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class BlogTagRepository : BaseRepository<BlogTag>, IBlogTagRepository
    {
        public BlogTagRepository(AcademicBlogContext context) : base(context)
        {
        }
    }
}
