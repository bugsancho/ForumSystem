namespace ForumSystem.Infrastructure.Cache
{
    using System;
    using System.Runtime.Caching;

    using ForumSystem.Core.Shared;

    class InMemoryCacheManager : ICacheManager
    {
        private readonly MemoryCache _memoryCache;

        public InMemoryCacheManager()
        {
            _memoryCache = new MemoryCache("ForumSystem");
        }

        public void Add<T>(string key, T value, DateTime expiration)
        {
            _memoryCache.Add(key, value, expiration);
        }

        public T Get<T>(string key) where T : class
        {
            object result =_memoryCache.Get(key);
            if (result != null)
            {
                return (T)result;
            }

            return null;
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
