namespace ForumSystem.Infrastructure.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Core.Data;
    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Entities.Base;

    public class EfRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly ForumSystemDbContext _dbContext;

        private readonly IDbSet<T> _set;

        public EfRepository(ForumSystemDbContext dbContext)
        {
            _dbContext = dbContext;
            _set = _dbContext.Set<T>();
        }


        public async Task<IReadOnlyCollection<T>> All()
        {
            return await _set.ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> All(PagingInfo pagingInfo)
        {
            int itemsToSkip = pagingInfo.Page - 1 * pagingInfo.PageSize;
            return await _set
                .Skip(itemsToSkip)
                .Take(pagingInfo.PageSize)
                .ToListAsync();
        }

        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            _set.Remove(entity);
        }

        public async Task<T> GetById(int id)
        {
            return await _set.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Add(T entity)
        {
            _set.Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
