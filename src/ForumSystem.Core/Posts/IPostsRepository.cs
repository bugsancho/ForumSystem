namespace ForumSystem.Core.Posts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ForumSystem.Core.Data;
    using ForumSystem.Core.Entities;

    public interface IPostsRepository : IRepository<ForumPost>
    {
        Task<IReadOnlyCollection<ForumPost>> GetByThread(int threadId);
    }
}
