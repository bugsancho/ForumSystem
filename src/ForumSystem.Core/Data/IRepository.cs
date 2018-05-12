namespace ForumSystem.Core.Data
{
    using System.Collections.Generic;

    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Entities.Base;

    public interface IRepository<T> where T : IEntity
    {
        IReadOnlyCollection<T> All();

        IReadOnlyCollection<T> All(PagingInfo pagingInfo);

        T GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(int id);

        void Dispose();

    }
}
