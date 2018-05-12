namespace ForumSystem.Core.Entities
{
    using System.Collections.Generic;

    using ForumSystem.Core.Entities.Base;

    public class ForumThread : BaseEntity
    {
        public ForumThread()
        {
            _forumPosts = new HashSet<ForumPost>();
        }
        private ICollection<ForumPost> _forumPosts;

        public string Title { get; set; }

        public virtual User User { get; set; }

        public int? UserId { get; set; }

        public virtual ICollection<ForumPost> ForumPosts
        {
            get => _forumPosts;
            set => _forumPosts = value;
        }
    }
}
