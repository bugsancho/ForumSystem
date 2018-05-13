namespace ForumSystem.Core.Threads
{
    using System;
    using System.Collections.Generic;

    using ForumSystem.Core.Entities;
    using ForumSystem.Core.Posts;

    public class ThreadDetailsModel
    {
        public ThreadDetailsModel(ForumThread thread, IReadOnlyCollection<PostDetailsModel> posts)
        {
            Id = thread.Id;
            Title = thread.Title;
            PublishedDate = thread.CreatedOn;
            PublisherUsername = thread.User?.Username;
            IsAnonymous = thread.UserId == null;
            Posts = posts;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishedDate { get; set; }

        public string PublisherUsername { get; set; }

        public bool IsAnonymous { get; set; }

        public IReadOnlyCollection<PostDetailsModel> Posts { get; set; }
    }
}
