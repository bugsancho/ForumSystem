namespace ForumSystem.Core.Threads
{
    using System.Threading.Tasks;

    using ForumSystem.Core.Data;
    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Posts;
    using ForumSystem.Core.Users;

    public class ForumThreadsService : IForumThreadsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IPostsService _postsService;

        private const int DefaultPageSize = 10;

        public ForumThreadsService(IUnitOfWork unitOfWork, IUserService userService, IPostsService postsService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _postsService = postsService;
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

        public async Task<ForumThread> Create(CreateThreadModel createModel)
        {
            ForumThread thread = new ForumThread
            {
                Title = createModel.Title
            };

            if (!string.IsNullOrWhiteSpace(createModel.Username))
            {
                User user = await _userService.GetByUsername(createModel.Username);
                thread.User = user;
                thread.UserId = user.Id;
            }

            // In order to ensure atomicity that a thread can only be created when the initial post is also created, we rely on transactions
            using (ITransaction transaction = _unitOfWork.BeginTransaction())
            {
                // Saving changes generates an ID for the thread taht we can use to attach the post
                _unitOfWork.ForumThreads.Add(thread);

                await _unitOfWork.SaveChanges();

                CreatePostModel createPostModel = createModel.InitialPost;
                createPostModel.ThreadId = thread.Id;
                createPostModel.Username = createPostModel.Username;

                await _postsService.CreatePost(createPostModel);

                transaction.Commit();
                return thread;
            }
        }
    }
}
