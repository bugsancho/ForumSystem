namespace ForumSystem.Core.Shared
{
    using System;

    public interface ICacheManager
    {
        void Add<T>(string key, T value, DateTime expiration);

        T Get<T>(string key) where T : class;

        void Remove(string key);

    }
}
