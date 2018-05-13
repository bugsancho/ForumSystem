namespace ForumSystem.Identity.Models
{
    using System.Data.Entity;

    using ForumSystem.Identity.Migrations;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class IdentityDbContext : IdentityDbContext<ApplicationIdentityUser>
    {
        public IdentityDbContext()
            : base("IdentityDbConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<IdentityDbContext, Configuration>());
        }


        public static IdentityDbContext Create()
        {
            return new IdentityDbContext();
        }

        public class SeedOnlyInitializer : IDatabaseInitializer<IdentityDbContext>
        {
            protected void Seed(ForumSystem.Identity.Models.IdentityDbContext context)
            {

                RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
                IdentityRole role = new IdentityRole { Name = "ThreadsAdmin" };

                roleManager.Create(role);

                context.SaveChanges();

                UserStore<ApplicationIdentityUser> userStore = new UserStore<ApplicationIdentityUser>(context);
                UserManager<ApplicationIdentityUser> userManager = new UserManager<ApplicationIdentityUser>(userStore);
                ApplicationIdentityUser user = new ApplicationIdentityUser { UserName = "admin@admin.com" };
                // Initial admin user
                userManager.Create(user, "admin123");
                userManager.AddToRole(user.Id, "ThreadsAdmin");

                context.SaveChanges();

            }

            public void InitializeDatabase(IdentityDbContext context)
            {
                Seed(context);
                context.SaveChanges();
            }
        }
    }
}