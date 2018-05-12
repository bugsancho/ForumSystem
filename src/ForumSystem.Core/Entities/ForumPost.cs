namespace ForumSystem.Core.Entities
{
    using ForumSystem.Core.Entities.Base;

    public class ForumPost : BaseEntity
    {
        public string Content { get; set; }

        public virtual ForumThread Thread { get; set; }

        public int ThreadId { get; set; }

        public virtual User User { get; set; }

        public int? UserId { get; set; }
    }
}
