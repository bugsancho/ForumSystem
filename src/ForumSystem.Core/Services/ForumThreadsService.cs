namespace ForumSystem.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ForumSystem.Core.Data;
    using ForumSystem.Core.Entities;

    public class ForumThreadsService : IForumThreadsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ForumThreadsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<ForumThread>> GetAll()
        {
            return await _unitOfWork.ForumThreads.All();
        }

        public async Task<ForumThread> GetById(int id)
        {
            return await _unitOfWork.ForumThreads.GetById(id);
        }

        public void Create(string title, ForumPost initialPost, User user = null)
        {
            ForumThread thread = new ForumThread
            {
                Title = title,
                User = user,
                UserId = user?.Id
            };

            thread.ForumPosts.Add(initialPost);

            _unitOfWork.ForumThreads.Add(thread);
            _unitOfWork.SaveChanges();
        }
    }
}
