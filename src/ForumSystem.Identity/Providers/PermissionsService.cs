namespace ForumSystem.Identity.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ForumSystem.Core.Users;
    using ForumSystem.Identity.Managers;

    public class PermissionsService : IPermissionsService
    {
        private readonly ApplicationUserManager _userManager;

        private const string CanEditThreadsRole = "ThreadsAdmin";

        public PermissionsService(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CanEditThreads(string username)
        {
            IList<string> roles = await _userManager.GetRolesAsync(username);
            return roles.Contains(CanEditThreadsRole);
        }
    }
}
