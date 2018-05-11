namespace ForumSystem.Identity.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class IdentityDbContext : IdentityDbContext<ApplicationIdentityUser>
    {
        public IdentityDbContext()
            : base("IdentityDbConnection", throwIfV1Schema: false)
        {
        }

        public static IdentityDbContext Create()
        {
            return new IdentityDbContext();
        }
    }
}