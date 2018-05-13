using System.Threading.Tasks;
using ForumSystem.Core.Entities;

namespace ForumSystem.Core.Users
{
    public interface IUserService
    {
        Task<User> GetByUsername(string username);
    }
}