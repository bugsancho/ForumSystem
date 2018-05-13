namespace ForumSystem.Core.Posts
{
    using System.Threading.Tasks;

    using ForumSystem.Core.Data;
    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Users;

    public class PostsService : IPostsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public PostsService(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<ForumPost> CreatePost(CreatePostModel createModel)
        {
            ForumPost post = new ForumPost
            {
                ThreadId = createModel.ThreadId,
                Content = createModel.Content
            };

            if (!string.IsNullOrWhiteSpace(createModel.Username))
            {
                User user = await _userService.GetByUsername(createModel.Username);
                post.User = user;
                post.UserId = user.Id;
            }

            await _unitOfWork.SaveChanges();

            return post;
        }
    }
}
