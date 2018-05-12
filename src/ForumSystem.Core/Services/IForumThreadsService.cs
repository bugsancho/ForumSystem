using System.Collections.Generic;
using ForumSystem.Core.Entities;

namespace ForumSystem.Core.Services
{
    using System.Threading.Tasks;

    public interface IForumThreadsService
    {
        Task<IReadOnlyCollection<ForumThread>> GetAll();

        Task<ForumThread> GetById(int id);

        void Create(string title, ForumPost initialPost, User user = null);
    }
}