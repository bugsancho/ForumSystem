using System.Collections.Generic;
using System.Web.Http;

namespace ForumSystem.Web.Api
{
    using System.Threading.Tasks;

    using ForumSystem.Core.Users;
    using ForumSystem.Web.Models;

    [Authorize]
    public class PermissionsController : ApiController
    {
        private readonly IPermissionsService _permissionsService;

        public PermissionsController(IPermissionsService permissionsService)
        {
            _permissionsService = permissionsService;
        }

        // GET api/<controller>
        public async Task<PermissionsModel> Get()
        {
            string username = User.Identity.Name;
            bool canEditThreads = await _permissionsService.CanEditThreads(username);
            PermissionsModel result = new PermissionsModel()
            {
                Username = username,
                CanEditThreads = canEditThreads
            };

            return result;
        }


    }
}