namespace ForumSystem.Infrastructure.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Entities.Base;

    public class ForumSystemDbContext : DbContext
    {
        // Empty contstructor to enable migrations
        public ForumSystemDbContext() :this("ForumSystemDbConnection")
        {
        }

        public ForumSystemDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public IDbSet<User> Users { get; set; }

        public IDbSet<ForumThread> Threads { get; set; }

        public IDbSet<ForumPost> Posts { get; set; }

        public override Task<int> SaveChangesAsync()
        {
            ApplyAuditInfoRules();
            ApplyDeletableEntityRules();

            return base.SaveChangesAsync();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (DbEntityEntry entry in
                ChangeTracker.Entries()
                    .Where(
                        e =>
                            e.Entity is IAuditedEntity && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                IAuditedEntity entity = (IAuditedEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                DbEntityEntry entry in
                ChangeTracker.Entries()
                    .Where(e => e.Entity is ISoftDeletableEntity && (e.State == EntityState.Deleted)))
            {
                ISoftDeletableEntity entity = (ISoftDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.UtcNow;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}
