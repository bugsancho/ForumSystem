namespace ForumSystem.Core.Entities
{
    using ForumSystem.Core.Entities.Base;

    public class User : BaseEntity
    {
        public string Username { get; set; }

        public string Random { get; set; }
    }
}
