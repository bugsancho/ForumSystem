using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ForumSystem.Web.Startup))]

namespace ForumSystem.Web
{
    using ForumSystem.Identity;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IdentityStartup.ConfigureAuth(app);
        }
    }
}
