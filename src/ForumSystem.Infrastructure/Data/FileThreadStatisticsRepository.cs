namespace ForumSystem.Infrastructure.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Core.Analytics;
    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Shared;

    using Newtonsoft.Json;

    public class FileThreadStatisticsRepository : IThreadStatisticsRepository
    {
        private readonly string _filePath;

        private readonly ICacheManager _cacheManager;

        private const string StatisticsCacheKey = "ThreadStatistics";
        public FileThreadStatisticsRepository(string filePath, ICacheManager cacheManager)
        {
            _filePath = filePath;
            _cacheManager = cacheManager;
        }

        private IReadOnlyCollection<ThreadStatistics> ReadFile()
        {
            bool fileExists = File.Exists(_filePath);
            if (!fileExists)
            {
                throw new FileNotFoundException("The Thread statistics file could not be found. Please provide correct file path.", _filePath);
            }

            string contents = File.ReadAllText(_filePath);
            IReadOnlyCollection<ThreadStatistics> threadStatistics = JsonConvert.DeserializeObject<IReadOnlyCollection<ThreadStatistics>>(contents);
            return threadStatistics;
        }


        private IReadOnlyCollection<ThreadStatistics> GetStatistics()
        {
            IReadOnlyCollection<ThreadStatistics> statistics = _cacheManager.Get<IReadOnlyCollection<ThreadStatistics>>(StatisticsCacheKey);
            if (statistics == null)
            {
                statistics = ReadFile();
                _cacheManager.Add(StatisticsCacheKey, statistics, DateTime.UtcNow.AddDays(1));
            }

            return statistics;

        }

        public async Task<IReadOnlyCollection<ThreadStatistics>> Get(int threadId, DateTime start, DateTime end)
        {
            IReadOnlyCollection<ThreadStatistics> statistics = GetStatistics();

            // For the sake of the demo, we ignore the threadId and awalys return the same statistics 
            List<ThreadStatistics> filteredStatistics = statistics.Where(x => x.Timestamp.Date > start.Date && x.Timestamp <= end.Date).ToList();

            return await Task.FromResult(filteredStatistics);
        }
    }
}
