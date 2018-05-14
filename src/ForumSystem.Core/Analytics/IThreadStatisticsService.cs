using System.Collections.Generic;
using System.Threading.Tasks;

namespace ForumSystem.Core.Analytics
{
    public interface IThreadStatisticsService
    {
        Task<IReadOnlyCollection<ThreadStatisticsResult>> Get(ThreadStatisticsRequest statisticsRequest);
    }
}