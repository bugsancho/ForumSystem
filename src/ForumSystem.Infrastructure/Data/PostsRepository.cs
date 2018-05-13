namespace ForumSystem.Infrastructure.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Posts;

    public class PostsRepository : EfRepository<ForumPost>, IPostsRepository
    {
        public PostsRepository(ForumSystemDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IReadOnlyCollection<ForumPost>> GetByThread(int threadId)
        {
            return await Set
                .Where(x => x.ThreadId == threadId)
                .OrderByDescending(x => x.CreatedOn)
                .Include(x => x.User)
                .ToListAsync();
        }
    }
}
