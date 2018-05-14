using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ForumSystem.Web.Api
{
    using System.Threading.Tasks;

    using ForumSystem.Core.Analytics;

    public class ThreadStatisticsController : ApiController
    {
        private readonly IThreadStatisticsService _statisticsService;

        public ThreadStatisticsController(IThreadStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        // GET api/<controller>
        public async Task<IReadOnlyCollection<ThreadStatisticsResult>> Get([FromUri]ThreadStatisticsRequest statisticsRequest)
        {
            if (statisticsRequest == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return await _statisticsService.Get(statisticsRequest);
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