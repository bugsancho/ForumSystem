namespace ForumSystem.Identity.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ForumSystem.Core.Users;
    using ForumSystem.Identity.Managers;
    using ForumSystem.Identity.Models;

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
            ApplicationIdentityUser user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new UserNotFoundException(username);
            }

            IList<string> roles = await _userManager.GetRolesAsync(user.Id);
            return roles.Contains(CanEditThreadsRole);
        }
    }
}
