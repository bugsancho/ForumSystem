using System.Threading.Tasks;
using ForumSystem.Core.Entities;

namespace ForumSystem.Core.Posts
{
    using System.Collections.Generic;

    public interface IPostsService
    {
        Task<PostDetailsModel> CreatePost(CreatePostModel createModel);

        Task<IReadOnlyCollection<PostDetailsModel>> GetPosts(int threadId);

        Task<PostDetailsModel> GetById(int id);

        Task<PostDetailsModel> UpdatePost(EditPostModel editModel);

        Task DeletePost(int postId);
    }
}