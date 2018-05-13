namespace ForumSystem.Identity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using ForumSystem.Identity.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<ForumSystem.Identity.Models.IdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ForumSystem.Identity.Models.IdentityDbContext";
        }

        protected override void Seed(ForumSystem.Identity.Models.IdentityDbContext context)
        {
            if (!context.Roles.Any(x => x.Name=="ThreadsAdmin"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = "ThreadsAdmin" };

                roleManager.Create(role);

                context.SaveChanges();


            }
            if (!context.Users.Any(x => x.UserName == "admin@admin.com"))
            {
                var userStore = new UserStore<ApplicationIdentityUser>(context);
                var userManager = new UserManager<ApplicationIdentityUser>(userStore);
                var user = new ApplicationIdentityUser { UserName = "admin@admin.com" };

                userManager.Create(user, "admin123");
                userManager.AddToRole(user.Id, "ThreadsAdmin");

                context.SaveChanges();


            }
        }
    }
}
