namespace ForumSystem.Core.Analytics
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ForumSystem.Core.Entities;

    public class ThreadStatisticsService : IThreadStatisticsService
    {
        private readonly IThreadStatisticsRepository _statisticsRepository;

        private const int NumberOfIntervalsToShow = 10;

        public ThreadStatisticsService(IThreadStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<IReadOnlyCollection<ThreadStatisticsResult>> Get(ThreadStatisticsRequest statisticsRequest)
        {
            DateTime startDate = GetStartDate(statisticsRequest.AggregationInterval);
            DateTime endDate = DateTime.UtcNow;
            IReadOnlyCollection<ThreadStatistics> statistics = await _statisticsRepository.Get(statisticsRequest.ThreadId, startDate, endDate);
            IEnumerable<IGrouping<DateTime, ThreadStatistics>> grouppedByPeriod = statistics.GroupBy(x => GetStartOfPeriod(x.Timestamp, statisticsRequest.AggregationInterval));
            List<ThreadStatisticsResult> results = new List<ThreadStatisticsResult>();

            foreach (IGrouping<DateTime, ThreadStatistics> periodGroup in grouppedByPeriod)
            {
                ThreadStatistics samplePoint = periodGroup.First();

                DateTime startOfPeriod = GetStartOfPeriod(samplePoint.Timestamp, statisticsRequest.AggregationInterval);
                string currentDateLabel = GetLabelForDate(startOfPeriod, statisticsRequest.AggregationInterval);

                ThreadStatisticsResult currentResult = new ThreadStatisticsResult
                {
                    Label = currentDateLabel
                };

                foreach (ThreadStatistics dataPoint in periodGroup)
                {
                    currentResult.AbsoluteHits += dataPoint.AbsoluteHits;
                    currentResult.Hits += dataPoint.Hits;
                }

                results.Add(currentResult);
            }

            return await Task.FromResult(results);
        }

        private DateTime GetStartOfPeriod(DateTime date, StatisticsAggregationInterval interval)
        {
            switch (interval)
            {
                case StatisticsAggregationInterval.Day:
                    return date;
                case StatisticsAggregationInterval.Week:
                    return date.StartOfWeek(DayOfWeek.Monday);
                case StatisticsAggregationInterval.Month:
                    return date.StartOfMonth();
                case StatisticsAggregationInterval.Year:
                    return date.StartOfYear();
                default:
                    throw new ArgumentOutOfRangeException(nameof(interval), interval, null);
            }
        }

        private string GetLabelForDate(DateTime date, StatisticsAggregationInterval interval)
        {
            switch (interval)
            {
                case StatisticsAggregationInterval.Day:
                    return date.ToString("dd/MM/yyyy");
                case StatisticsAggregationInterval.Week:
                    return date.ToString("dd/MM/yyyy");
                case StatisticsAggregationInterval.Month:
                    return date.ToString("Y");
                case StatisticsAggregationInterval.Year:
                    return date.Year.ToString();
                default:
                    throw new ArgumentOutOfRangeException(nameof(interval), interval, null);
            }
        }

        //private IEnumerable<IGrouping<DateTime, ThreadStatistics>> GroupStatistics(IReadOnlyCollection<ThreadStatistics> statistics, StatisticsAggregationInterval aggregationInterval)
        //{

        //    switch (aggregationInterval)
        //    {
        //        //Statistics are already grouped by day
        //        case StatisticsAggregationInterval.Day:
        //            return statistics.GroupBy(x => x.Timestamp.Date);
        //        case StatisticsAggregationInterval.Week:
        //            return statistics.GroupBy(x => x.Timestamp.StartOfWeek(DayOfWeek.Monday));
        //        case StatisticsAggregationInterval.Month:
        //            return statistics.GroupBy(x => x.Timestamp.StartOfMonth());
        //        case StatisticsAggregationInterval.Year:
        //            return statistics.GroupBy(x => x.Timestamp.StartOfYear());

        //        default:
        //            throw new ArgumentOutOfRangeException(nameof(aggregationInterval), aggregationInterval, null);
        //    }
        //}



        private DateTime GetStartDate(StatisticsAggregationInterval aggregationInterval)
        {
            switch (aggregationInterval)
            {
                case StatisticsAggregationInterval.Day:
                    return DateTime.UtcNow.AddDays(-NumberOfIntervalsToShow);
                case StatisticsAggregationInterval.Week:
                    return DateTime.UtcNow.AddDays(-(7 * NumberOfIntervalsToShow));
                case StatisticsAggregationInterval.Month:
                    return DateTime.UtcNow.AddMonths(-NumberOfIntervalsToShow);
                case StatisticsAggregationInterval.Year:
                    return DateTime.UtcNow.AddYears(-NumberOfIntervalsToShow);

                default:
                    throw new ArgumentOutOfRangeException(nameof(aggregationInterval), aggregationInterval, null);
            }
        }


    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime StartOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime StartOfYear(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 1, 1);
        }
    }
}
