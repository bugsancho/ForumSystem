namespace ForumSystem.Core.Entities
{
    using System.Collections.Generic;

    public class PagedResult<T>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public IReadOnlyCollection<T> Results { get; set; }

        public int AvailablePages { get; set; }

        public int TotalCount { get; set; }


    }
}
