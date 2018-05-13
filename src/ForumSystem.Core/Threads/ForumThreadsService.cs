namespace ForumSystem.Core.Threads
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Core.Data;
    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Posts;
    using ForumSystem.Core.Shared;
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

        public async Task<PagedResult<ThreadDetailsModel>> GetAll(PagingInfo pagingInfo = null)
        {
            if (pagingInfo == null)
            {
                pagingInfo = new PagingInfo
                {
                    PageSize = DefaultPageSize,
                    Page = 1
                };
            }

            PagedResult<ForumThread> threads = await _unitOfWork.ForumThreads.All(pagingInfo, thread => thread.User);
            IReadOnlyCollection<ThreadDetailsModel> threadDetails = threads.Results.Select(x => new ThreadDetailsModel(x, new List<PostDetailsModel>())).ToList();
            PagedResult<ThreadDetailsModel> pagedResult = new PagedResult<ThreadDetailsModel>
            {
                Results = threadDetails,
                Page = threads.Page,
                PageSize = threads.PageSize,
                AvailablePages = threads.AvailablePages,
                TotalCount = threads.TotalCount
            };

            return pagedResult;
        }

        public async Task<ThreadDetailsModel> GetById(int id)
        {
            // Get the thread and then retrieve the posts associated with it
            ForumThread thread = await _unitOfWork.ForumThreads.GetById(id);
            IReadOnlyCollection<PostDetailsModel> posts = await _postsService.GetPosts(id);

            // Combine the retrieved thread and the posts
            ThreadDetailsModel threadDetails = new ThreadDetailsModel(thread, posts);
            return threadDetails;
        }
         
        /// <summary>
        /// Creates a new thread along with the initial post.
        /// </summary>
        /// <param name="createModel">The model of the new thread</param>
        /// <returns>The id of the newly created thread</returns>
        public async Task<EntityCreatedResult> Create(CreateThreadModel createModel)
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
                createPostModel.Username = createModel.Username;

                await _postsService.CreatePost(createPostModel);


                transaction.Commit();

                return new EntityCreatedResult { Id = thread.Id };
            }
        }
    }
}
