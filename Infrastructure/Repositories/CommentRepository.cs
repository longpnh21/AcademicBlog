using Core.Entities;
using Core.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AcademicBlogContext context) : base(context) { }

        public async Task<IEnumerable<Comment>> GetAllReply(int id)
            => await _context.Set<Comment>().AsQueryable().AsNoTracking().Include(r => r.User).Where(r => r.ReferenceId == id).ToListAsync();

        public async Task<IEnumerable<Comment>> GetAllCommentFromPost(int id) =>
            await _context.Set<Comment>().AsQueryable().AsNoTracking().Include(r => r.User).Where(r => r.BlogId == id && r.ReferenceId == null).ToListAsync();
    }
}
