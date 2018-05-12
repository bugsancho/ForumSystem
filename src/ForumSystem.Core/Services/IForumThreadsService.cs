using System.Collections.Generic;
using ForumSystem.Core.Entities;

namespace ForumSystem.Core.Services
{
    public interface IForumThreadsService
    {
        IReadOnlyCollection<ForumThread> GetAll();

        ForumThread GetById(int id);

        void Create(string title, ForumPost initialPost, User user = null);
    }
}