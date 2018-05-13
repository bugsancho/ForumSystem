namespace ForumSystem.Infrastructure.Data
{
    using System.Data.Entity;

    using ForumSystem.Core.Data;

    public class EfTransaction : ITransaction
    {
        private readonly DbContextTransaction _efTransaction;

        public EfTransaction(DbContextTransaction efTransaction)
        {
            _efTransaction = efTransaction;
        }

        public void Dispose()
        {
            _efTransaction.Dispose();
        }

        public void Commit()
        {
            _efTransaction.Commit();
        }

        public void Rollback()
        {
            _efTransaction.Rollback();
        }
    }
}
