
namespace ForumSystem.Core.Data
{
    using System.Threading.Tasks;

    using ForumSystem.Core.Entities;

    public interface IUnitOfWork
    {
        IRepository<ForumThread> ForumThreads { get; }

        IRepository<ForumPost> ForumPosts { get; }

        IUserRepository Users { get; }

        Task SaveChanges();

    }
}
