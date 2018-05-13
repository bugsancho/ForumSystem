using System.Threading.Tasks;
using ForumSystem.Core.Entities;

namespace ForumSystem.Core.Users
{
    using ForumSystem.Core.Shared;

    public interface IUserService
    {
        Task<User> GetByUsername(string username);

        Task<EntityCreatedResult> Create(User user);
    }
}