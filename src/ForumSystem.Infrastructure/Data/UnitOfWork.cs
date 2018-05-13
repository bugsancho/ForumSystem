namespace ForumSystem.Infrastructure.Data
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using ForumSystem.Core.Data;
    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Posts;
    using ForumSystem.Core.Users;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ForumSystemDbContext _dbContext;

        public UnitOfWork(ForumSystemDbContext dbContext, IRepository<ForumThread> forumThreads, IPostsRepository forumPosts, IUserRepository users)
        {
            _dbContext = dbContext;
            ForumThreads = forumThreads;
            ForumPosts = forumPosts;
            Users = users;
        }

        public IRepository<ForumThread> ForumThreads { get; }

        public IPostsRepository ForumPosts { get; }

        public IUserRepository Users { get; }

        public ITransaction BeginTransaction()
        {
            DbContextTransaction dbContextTransaction = _dbContext.Database.BeginTransaction();
            return new EfTransaction(dbContextTransaction);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
