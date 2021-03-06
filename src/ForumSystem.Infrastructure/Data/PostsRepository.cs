﻿namespace ForumSystem.Infrastructure.Data
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
            return await Set.Where(x => x.IsDeleted == false).Where(x => x.ThreadId == threadId)
                            .OrderBy(x => x.CreatedOn).Include(x => x.User).ToListAsync();
        }
    }
}
