
namespace ForumSystem.Core.Data
{
    using System.Threading.Tasks;

    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Posts;
    using ForumSystem.Core.Users;

    public interface IUnitOfWork
    {
        IRepository<ForumThread> ForumThreads { get; }

        IPostsRepository ForumPosts { get; }

        IUserRepository Users { get; }

        Task SaveChanges();

        ITransaction BeginTransaction();
    }
}
