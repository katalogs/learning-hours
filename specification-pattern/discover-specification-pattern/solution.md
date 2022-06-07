- For the `Blog` part

```c#
namespace Blog.Blogs
{
    public class ActivatedBlogSpecification : Specification<Blog>
    {
        public override Expression<Func<Blog, bool>> ToExpression() 
            => blog => blog.Activated.HasValue && IsAccessible(blog);

        private bool IsAccessible(Blog blog) 
            => !blog.Banned.HasValue 
               && !blog.Removed.HasValue;
    }
}
```

- For the `Post` part


```c#
public class ActivatedSpecification : Specification<Post>
{
    public override Expression<Func<Post, bool>> ToExpression() => post => post.Activated.HasValue;
}

public class CreatedAfterSpecification : Specification<Post>
{
    private readonly DateTime _date;

    public CreatedAfterSpecification(DateTime date) => _date = date;

    public override Expression<Func<Post, bool>> ToExpression() => post => post.Created >= _date;
}

public class NotBannedSpecification : Specification<Post>
{
    public override Expression<Func<Post, bool>> ToExpression() => post => !post.Banned.HasValue;
}

public class NotRemovedSpecification : Specification<Post>
{
    public override Expression<Func<Post, bool>> ToExpression() => post => !post.Removed.HasValue;
}
```