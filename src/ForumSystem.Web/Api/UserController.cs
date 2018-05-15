namespace ForumSystem.Web.Api
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using ForumSystem.Core.Users;
    using ForumSystem.Web.Models;

    [Authorize]
    public class UserController : ApiController
    {
        private readonly IPermissionsService _permissionsService;

        public UserController(IPermissionsService permissionsService)
        {
            _permissionsService = permissionsService;
        }

        // GET api/<controller>
        public async Task<UserDetailsModel> Get()
        {
            string username = User.Identity.Name;
            bool canEditThreads = await _permissionsService.CanEditThreads(username);
            UserDetailsModel result = new UserDetailsModel
            {
                Username = username,
                CanEditThreads = canEditThreads
            };

            return result;
        }


    }
}