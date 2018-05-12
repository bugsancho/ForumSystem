
namespace ForumSystem.Core.Data
{
    using ForumSystem.Core.Entities;

    public interface IUnitOfWork
    {
        IRepository<ForumThread> ForumThreads { get; }

        IRepository<ForumPost> ForumPosts { get; }

        IUserRepository Users { get; }

        void SaveChanges();

    }
}
