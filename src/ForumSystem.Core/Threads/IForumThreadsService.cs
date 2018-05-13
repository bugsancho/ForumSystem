namespace ForumSystem.Core.Threads
{
    using System.Threading.Tasks;

    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Shared;

    public interface IForumThreadsService
    {
        Task<PagedResult<ThreadDetailsModel>> GetAll(PagingInfo pagingInfo = null);

        Task<ThreadDetailsModel> GetById(int id);

        Task<EntityCreatedResult> Create(CreateThreadModel createModel);
    }
}