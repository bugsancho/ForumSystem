using System.Threading.Tasks;
using ForumSystem.Core.Entities;

namespace ForumSystem.Core.Posts
{
    public interface IPostsService
    {
        Task<ForumPost> CreatePost(CreatePostModel createModel);
    }
}