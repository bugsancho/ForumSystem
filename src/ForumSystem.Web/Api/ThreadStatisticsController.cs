using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            if (statisticsRequest == null || statisticsRequest.ThreadId <= 0 || statisticsRequest.AggregationInterval == StatisticsAggregationInterval.None)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return await _statisticsService.Get(statisticsRequest);
        }
    }
}