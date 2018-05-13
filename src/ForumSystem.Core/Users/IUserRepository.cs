namespace ForumSystem.Core.Users
{
    using System.Threading.Tasks;

    using ForumSystem.Core.Data;
    using ForumSystem.Core.Entities;

    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsername(string email);
    }
}
