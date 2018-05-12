namespace ForumSystem.Core.Data
{
    using ForumSystem.Core.Entities;

    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
    }
}
