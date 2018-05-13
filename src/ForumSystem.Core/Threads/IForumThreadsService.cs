namespace ForumSystem.Core.Threads
{
    using System.Threading.Tasks;

    using ForumSystem.Core.Entities;

    public interface IForumThreadsService
    {
        Task<PagedResult<ForumThread>> GetAll(PagingInfo pagingInfo = null);

        Task<ForumThread> GetById(int id);

        Task<ForumThread> Create(CreateThreadModel createModel);
    }
}