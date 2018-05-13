namespace ForumSystem.Core.Entities
{
    using System;

    public class ThreadStatistics
    {
        public int ThreadId { get; set; }

        public virtual ForumThread Thread { get; set; }

        public DateTime Timestamp { get; set; }

        public long Hits { get; set; }

        public long AbsoluteHits { get; set; }
    }
}
