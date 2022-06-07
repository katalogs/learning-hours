using Blog.Posts;

namespace Blog
{
    public class BlogServiceV1
    {
        private readonly IReadRepository<Blogs.Blog> _blogRepository;
        public BlogServiceV1(IReadRepository<Blogs.Blog> repository) => _blogRepository = repository;

        public int CountValidPostsAfter(DateTime postedAfter) => 
            Filter(
                _blogRepository.FetchAll(),
                postedAfter
            ).Length;

        private Post[] Filter(Blogs.Blog[] blogs, DateTime postedAfter)
        {
            var results = new List<Post>();
        
            foreach (var blog in blogs)
            {
                if (!blog.Banned.HasValue)
                {
                    if (blog.Activated.HasValue)
                    {
                        if (!blog.Removed.HasValue)
                        {
                            if (!blog.Banned.HasValue)
                            {
                                foreach (var post in blog.Posts)
                                {
                                    if (!post.Banned.HasValue)
                                    {
                                        if (post.Activated.HasValue)
                                        {
                                            if (!post.Removed.HasValue)
                                            {
                                                if (!post.Banned.HasValue)
                                                {
                                                    if (post.Created.CompareTo(postedAfter) == 1)
                                                    {
                                                        results.Add(post);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return results.ToArray();
        }
    }
}