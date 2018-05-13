namespace ForumSystem.Infrastructure.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Core.Data;
    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Entities.Base;

    public class EfRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly ForumSystemDbContext DbContext;

        protected readonly IDbSet<T> Set;

        public EfRepository(ForumSystemDbContext dbContext)
        {
            DbContext = dbContext;
            Set = DbContext.Set<T>();
        }


        public async Task<IReadOnlyCollection<T>> All()
        {
            return await Set.ToListAsync();
        }

        public async Task<PagedResult<T>> All(PagingInfo pagingInfo)
        {
            int itemsToSkip = (pagingInfo.Page - 1) * pagingInfo.PageSize;

            List<T> results = await Set.OrderBy(x => x.Id).Skip(itemsToSkip).Take(pagingInfo.PageSize).ToListAsync();

            int totalCount = await Set.CountAsync();
            int availablePages = (int)Math.Ceiling(totalCount / (decimal)pagingInfo.PageSize);

            PagedResult<T> result = new PagedResult<T>
                             {
                                 PageSize = pagingInfo.PageSize,
                                 Page = pagingInfo.Page,
                                 Results = results,
                                 TotalCount = totalCount,
                                 AvailablePages = availablePages
            };

            return result;
        }

        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            Set.Remove(entity);
        }

        public async Task<T> GetById(int id)
        {
            return await Set.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Add(T entity)
        {
            Set.Add(entity);
        }

        public void Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
