namespace ForumSystem.Core.Posts
{
    public class CreatePostModel
    {
        public int ThreadId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }
    }
}
