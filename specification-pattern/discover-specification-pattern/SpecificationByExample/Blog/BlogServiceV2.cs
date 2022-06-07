namespace Blog
{
    public class BlogServiceV2
    {
        private readonly IReadRepository<Blogs.Blog> _blogRepository;
        public BlogServiceV2(IReadRepository<Blogs.Blog> repository) => _blogRepository = repository;

        public int CountValidPostsAfter(DateTime postedAfter)
            => throw new NotImplementedException();
        // _blogRepository
        // .FetchAll()
        // .Where(blog => new ActivatedBlogSpecification().IsSatisfiedBy(blog))
        // .SelectMany(blog => blog.Posts)
        // .Count(post => new ActivatedSpecification()
        //     .And(new NotRemovedSpecification())
        //     .And(new NotBannedSpecification())
        //     .And(new CreatedAfterSpecification(postedAfter)).IsSatisfiedBy(post));
    }
}