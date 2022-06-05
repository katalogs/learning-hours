using Blog.Posts;

namespace Blog.Blogs
{
    public class Blog
    {
        public string Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Activated { get; set; }
        public DateTime? Removed { get; set; }
        public DateTime? Banned { get; set; }

        public Post[] Posts { get; set; }
    }
}