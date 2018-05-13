namespace ForumSystem.Infrastructure.Migrations
{
    using System.Data.Entity.Migrations;

    using ForumSystem.Core.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<ForumSystem.Infrastructure.Data.ForumSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ForumSystem.Infrastructure.Data.ForumSystemDbContext context)
        {
            context.Users.Add(new User { Username = "admin@admin.com" });
        }
    }
}
