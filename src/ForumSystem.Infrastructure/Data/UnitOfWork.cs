namespace ForumSystem.Infrastructure.Data
{
    using System.Threading.Tasks;

    using ForumSystem.Core.Data;
    using ForumSystem.Core.Entities;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ForumSystemDbContext _dbContext;

        public UnitOfWork(ForumSystemDbContext dbContext, IRepository<ForumThread> forumThreads, IRepository<ForumPost> forumPosts, IUserRepository users)
        {
            _dbContext = dbContext;
            ForumThreads = forumThreads;
            ForumPosts = forumPosts;
            Users = users;
        }

        public IRepository<ForumThread> ForumThreads { get; }

        public IRepository<ForumPost> ForumPosts { get; }

        public IUserRepository Users { get; }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
