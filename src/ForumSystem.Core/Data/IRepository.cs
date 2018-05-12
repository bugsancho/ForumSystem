namespace ForumSystem.Core.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Entities.Base;

    public interface IRepository<T> where T : class, IEntity
    {
        Task<IReadOnlyCollection<T>> All();

        Task<PagedResult<T>> All(PagingInfo pagingInfo);

        Task<T> GetById(int id);

        void Add(T entity);

        void Update(T entity);

        Task Delete(int id);
    }
}
