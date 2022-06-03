namespace Blog
{
    public interface IReadRepository<T>
    {
        Blogs.Blog[] FetchAll();
    }
}