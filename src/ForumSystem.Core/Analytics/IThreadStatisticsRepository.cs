namespace ForumSystem.Core.Analytics
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ForumSystem.Core.Entities;

    public interface IThreadStatisticsRepository
    {
        Task<IReadOnlyCollection<ThreadStatistics>> Get(int threadId, DateTime start, DateTime end);

    }
}
