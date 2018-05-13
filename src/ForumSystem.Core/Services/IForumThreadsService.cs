using System.Collections.Generic;
using ForumSystem.Core.Entities;

namespace ForumSystem.Core.Services
{
    using System.Threading.Tasks;

    public interface IForumThreadsService
    {
        Task<PagedResult<ForumThread>> GetAll(PagingInfo pagingInfo = null);

        Task<ForumThread> GetById(int id);

        Task<ForumThread> Create(string title, ForumPost initialPost, User user = null);
    }
}