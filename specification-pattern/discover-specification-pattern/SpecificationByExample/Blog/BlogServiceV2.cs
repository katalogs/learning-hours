namespace Blog
{
    public class BlogServiceV2
    {
        private readonly IReadRepository<Blogs.Blog> _blogRepository;
        public BlogServiceV2(IReadRepository<Blogs.Blog> repository) => _blogRepository = repository;

        public int CountValidPostsAfter(DateTime postedAfter)
            => throw new NotImplementedException();
    }
}