namespace ForumSystem.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ForumSystem.Core.Data;
    using ForumSystem.Core.Entities;

    public class ForumThreadsService : IForumThreadsService
    {
        private readonly IUnitOfWork _unitOfWork;

        private const int DefaultPageSize = 10;

        public ForumThreadsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<ForumThread>> GetAll(PagingInfo pagingInfo = null)
        {
            if (pagingInfo == null)
            {
                pagingInfo = new PagingInfo
                {
                    PageSize = DefaultPageSize,
                    Page = 1
                };
            }

            return await _unitOfWork.ForumThreads.All(pagingInfo);
        }

        public async Task<ForumThread> GetById(int id)
        {
            return await _unitOfWork.ForumThreads.GetById(id);
        }

        public async Task<ForumThread> Create(string title, ForumPost initialPost, User user = null)
        {
            ForumThread thread = new ForumThread
            {
                Title = title,
                User = user,
                UserId = user?.Id
            };

            thread.ForumPosts.Add(initialPost);

            _unitOfWork.ForumThreads.Add(thread);
            await _unitOfWork.SaveChanges();

            return thread;
        }
    }
}
