namespace ForumSystem.Core.Data
{
    using System.Threading.Tasks;

    using ForumSystem.Core.Entities;

    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}
