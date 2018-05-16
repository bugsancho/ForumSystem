namespace ForumSystem.Core.IntegrationTests.Base
{
    using System.Data.Entity;

    using ForumSystem.Infrastructure.Data;

    using NUnit.Framework;

    public class IntegrationTestBase
    {
        protected ForumSystemDbContext DbContext;

        protected DbContextTransaction Transaction;

        public virtual void Setup()
        {
            DbContext = new ForumSystemDbContext("TestDb");
            DbContext.Database.CreateIfNotExists();
            Transaction = DbContext.Database.BeginTransaction();
        }

        [TearDown]
        public void TestCleanup()
        {
            Transaction.Dispose();
        }
    }
}
