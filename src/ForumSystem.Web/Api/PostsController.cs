using System.Collections.Generic;
using System.Web.Http;

namespace ForumSystem.Web.Api
{
    using System.Net;
    using System.Threading.Tasks;

    using ForumSystem.Core.Posts;

    public class PostsController : ApiController
    {
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        // GET: api/Posts
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Posts/5
        public async Task<PostDetailsModel> Get(int id)
        {
            return await _postsService.GetById(id);
        }

        // POST: api/Posts
        public async Task<PostDetailsModel> Post([FromBody]CreatePostModel createModel)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    createModel.Username = User.Identity.Name;
                }

                return await _postsService.CreatePost(createModel);
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // PUT: api/Posts/5
        [Authorize]
        public async Task<PostDetailsModel> Put(int id, [FromBody]EditPostModel editModel)
        {

            if (ModelState.IsValid)
            {
                editModel.PostId = id;
                return await _postsService.UpdatePost(editModel);
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);

        }

        // DELETE: api/Posts/5
        [Authorize]
        public async Task Delete(int id)
        {
            await _postsService.DeletePost(id);
        }
    }
}
