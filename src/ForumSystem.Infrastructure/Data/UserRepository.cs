namespace ForumSystem.Infrastructure.Data
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using ForumSystem.Core.Data;
    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Users;

    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(ForumSystemDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<User> GetByUsername(string email)
        {
            return await Set.FirstOrDefaultAsync(x => x.Username == email);
        }
    }
}
