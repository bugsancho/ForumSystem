using System.Threading.Tasks;
using ForumSystem.Core.Entities;

namespace ForumSystem.Core.Posts
{
    using System.Collections.Generic;

    public interface IPostsService
    {
        Task<ForumPost> CreatePost(CreatePostModel createModel);

        Task<IReadOnlyCollection<PostDetailsModel>> GetPosts(int threadId);
    }
}