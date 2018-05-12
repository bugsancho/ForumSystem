﻿using System.Collections.Generic;
using System.Web.Http;

namespace ForumSystem.Web.Api
{
    using System.Threading.Tasks;

    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Services;

    [Authorize]
    public class ForumThreadsController : ApiController
    {
        private readonly IForumThreadsService _threadsService;

        public ForumThreadsController(IForumThreadsService threadsService)
        {
            _threadsService = threadsService;
        }

        // GET api/<controller>
        public  async Task<IReadOnlyCollection<ForumThread>> Get()
        {
            return await _threadsService.GetAll();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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