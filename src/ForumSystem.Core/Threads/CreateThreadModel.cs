namespace ForumSystem.Core.Threads
{
    using ForumSystem.Core.Posts;

    public class CreateThreadModel
    {
        public string Title { get; set; }

        public string Username { get; set; }

        public CreatePostModel InitialPost { get; set; }

    }
}
