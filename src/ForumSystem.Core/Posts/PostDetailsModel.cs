namespace ForumSystem.Core.Posts
{
    using System;

    using ForumSystem.Core.Entities;

    public class PostDetailsModel
    {
        public PostDetailsModel(ForumPost forumPost)
        {
            Id = forumPost.Id;
            ThreadId = forumPost.ThreadId;
            Content = forumPost.Content;
            PublishedDate = forumPost.CreatedOn;
            PublisherUsername = forumPost.User?.Username;
            IsAnonymous = forumPost.UserId == null;
        }

        public int Id { get; set; }

        public int ThreadId { get; set; }

        public string Content { get; set; }

        public DateTime PublishedDate { get; set; }

        public string PublisherUsername { get; set; }

        public bool IsAnonymous { get; set; }
    }
}
