

namespace ForumSystem.Core.Analytics
{
    public class ThreadStatisticsRequest
    {
        public int ThreadId { get; set; }

        public StatisticsAggregationInterval AggregationInterval { get; set; }
    }
}