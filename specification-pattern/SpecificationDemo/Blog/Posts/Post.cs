namespace Blog.Posts
{
    public class Post
    {
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Activated { get; set; }
        public DateTime? Removed { get; set; }
        public DateTime? Banned { get; set; }
    }
}