namespace ForumSystem.Core.IntegrationTests.Base
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using ForumSystem.Infrastructure.Data;

    using NUnit.Framework;

    public class IntegrationTestBase
    {
        protected ForumSystemDbContext DbContext;

        protected DbContextTransaction Transaction;

        protected const string DbName = "TestDb";

        public virtual  void Setup()
        {
            DbContext = new ForumSystemDbContext(DbName);
            DbContext.Database.CreateIfNotExists();
            //
            Transaction = DbContext.Database.BeginTransaction();

        }

        /// <summary>
        /// Performs data seed on a separate instance of the db context
        /// In order to isolate the EF context cache, we need to perform the data seeding and data Querying on separate contexts
        /// Otherwise we might get false positives on tests that rely on internal cache</summary>
        protected async Task SeedData(Action<ForumSystemDbContext> seedDataAction)
        {
            using (var context = new ForumSystemDbContext(DbName))
            {
                seedDataAction(context);
                await context.SaveChangesAsync();
            }
        }

        [TearDown]
        public void TestCleanup()
        {
            Transaction.Dispose();
        }
    }
}
