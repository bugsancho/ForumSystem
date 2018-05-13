namespace ForumSystem.Core.Users
{
    using System.Threading.Tasks;

    public interface IPermissionsService
    {
        Task<bool> CanEditThreads(string username);
    }
}
