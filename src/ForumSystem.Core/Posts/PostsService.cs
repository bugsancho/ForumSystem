namespace ForumSystem.Core.Posts
{
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task<PostDetailsModel> CreatePost(CreatePostModel createModel)
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

            _unitOfWork.ForumPosts.Add(post);
            await _unitOfWork.SaveChanges();

            PostDetailsModel postDetails = new PostDetailsModel(post);
            return postDetails;
        }

        public async Task<IReadOnlyCollection<PostDetailsModel>> GetPosts(int threadId)
        {
            IReadOnlyCollection<ForumPost> posts = await _unitOfWork.ForumPosts.GetByThread(threadId);
            List<PostDetailsModel> postDetails = posts.Select(x => new PostDetailsModel(x)).ToList();
            return postDetails;
        }

        public async Task<PostDetailsModel> GetById(int id)
        {
            ForumPost post = await _unitOfWork.ForumPosts.GetById(id);
            PostDetailsModel postDetails = new PostDetailsModel(post);

            return postDetails;

        }

        public async Task<PostDetailsModel> UpdatePost(EditPostModel editModel)
        {
            ForumPost post = await _unitOfWork.ForumPosts.GetById(editModel.PostId);
            post.Content = editModel.Content;

            _unitOfWork.ForumPosts.Update(post);

            await _unitOfWork.SaveChanges();

            PostDetailsModel postDetails = new PostDetailsModel(post);

            return postDetails;
        }

        public async Task DeletePost(int postId)
        {
            await _unitOfWork.ForumPosts.Delete(postId);
            await _unitOfWork.SaveChanges();
        }
    }
}
