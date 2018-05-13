namespace ForumSystem.Core.Entities
{
    using System;

    public class PagingInfo
    {
        private int _page;

        private int _pageSize;

        /// <summary>
        /// Number of the requested page, where first page = 1
        /// </summary>
        public int Page
        {
            get => _page;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Page must be a positive number");
                }
                _page = value;
            }
        }

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("PageSize must be a positive number");
                }
                _pageSize = value;
            }
        }
    }
}
