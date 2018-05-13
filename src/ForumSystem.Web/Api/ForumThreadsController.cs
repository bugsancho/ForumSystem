using System.Collections.Generic;
using System.Web.Http;

namespace ForumSystem.Web.Api
{
    using System.Net;
    using System.Threading.Tasks;

    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Shared;
    using ForumSystem.Core.Threads;

    public class ForumThreadsController : ApiController
    {
        private readonly IForumThreadsService _threadsService;

        public ForumThreadsController(IForumThreadsService threadsService)
        {
            _threadsService = threadsService;
        }

        // GET api/<controller>
        public async Task<PagedResult<ThreadDetailsModel>> Get()
        {
            return await _threadsService.GetAll();
        }

        // GET api/<controller>/5
        public async Task<ThreadDetailsModel> Get(int id)
        {
            return await _threadsService.GetById(id);

        }

        // POST api/<controller>
        public async Task<EntityCreatedResult> Post([FromBody]CreateThreadModel createModel)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    createModel.Username = User.Identity.Name;
                }

                return await _threadsService.Create(createModel);
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}