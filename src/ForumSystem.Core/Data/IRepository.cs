namespace ForumSystem.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Entities.Base;

    public interface IRepository<T> where T : class, IEntity
    {
        Task<IReadOnlyCollection<T>> All();

        Task<PagedResult<T>> All<TProp>(PagingInfo pagingInfo, params Expression<Func<T, TProp>>[] propertiesToInclude);

        Task<T> GetById(int id);

        Task<T> GetById<TProp>(int id, params Expression<Func<T, TProp>>[] propertiesToInclude);

        void Add(T entity);

        void Update(T entity);

        Task Delete(int id);

    }
}
