# Specification Pattern
## Learning Goals
Understand how we could simplify `Specifications` and code readability

## Connect - Improvement ? (5 min)
- Identify what could make this code more readable 
- You can write a new version of it

```c#
public int Filter(DateTime postedAfter)
            => _blogRepository
                .FetchAll()
                .Where(blog => new ActivatedBlogSpecification().IsSatisfiedBy(blog))
                .SelectMany(blog => blog.Posts)
                .Count(post => new ActivatedSpecification()
                    .And(new NotRemovedSpecification())
                    .And(new NotBannedSpecification())
                    .And(new CreatedAfterSpecification(postedAfter)).IsSatisfiedBy(post));
```

![Improvement](../img/improvement.jpg)

## Concepts (10 min)
- We can improve the usage and readability of `Specification` code by creating extensions
  - For `Linq` for example
  - You can extend repository to do so

```c#
public static class QueryExtensions
{
    public static int Count<TSource>(this IEnumerable<TSource> source, Specification<TSource> specification)
    => source.Count(specification.IsSatisfiedBy);

    public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source,
        Specification<TSource> specification)
        => source.Where(specification.IsSatisfiedBy);
}
```

- Let's take a few minutes to inspect the new `Specification` code
```c#
public int CountValidPostsAfter(DateTime postedAfter)
    => _blogRepository
        .FetchAll()
        .Where(_specifications.For<Blogs.Blog>()
            .Activated())
        .SelectMany(blog => blog.Posts)
        .Count(_specifications.For<Post>()
            .Activated()
            .NotRemoved()
            .NotBanned()
            .CreatedAfter(postedAfter));
```
- It works with extension methods on `ISpecification<T>`
```c#
public static class DomainSpecifications
{
    public static ISpecification<Blogs.Blog> Activated(this ISpecification<Blogs.Blog> specification)
        => specification.And(blog => blog.Activated.HasValue && IsAccessible(blog));

    private static bool IsAccessible(Blogs.Blog blog)
        => !blog.Banned.HasValue
           && !blog.Removed.HasValue;

    public static ISpecification<Post> Activated(this ISpecification<Post> specification)
        => specification.And(post => post.Activated.HasValue);

    public static ISpecification<Post> NotRemoved(this ISpecification<Post> specification)
        => specification.And(post => !post.Removed.HasValue);

    public static ISpecification<Post> NotBanned(this ISpecification<Post> specification)
        => specification.And(post => !post.Banned.HasValue);

    public static ISpecification<Post> CreatedAfter(this ISpecification<Post> specification, DateTime date)
        => specification.And(post => post.Created >= date);
} 
```

## Concrete Practice (35 min)
- Open the `MovieKata` solution
- Implement the missing `Specification(s)` to pass the tests

```text
Given a customer living in Russia and major When they wants to play a movie on Putin Then they gets a rejection
Given a customer living in France and major When they wants to play a movie on Putin Then they gets an approval
Given a minor customer When they wants to play an "olé olé" movie Then they gets a rejection
Given a major customer When they wants to play an "olé olé" movie Then they gets an approval
```
[![Olé olé](../img/olé-olé.jpeg)](https://youtu.be/8bDmeeGVNvc)

- Which other rules / specification could be added?
  - More granular regarding [MPAA](https://en.wikipedia.org/wiki/Motion_Picture_Association_film_rating_system) maybe?

## Conclusion (5 min) - Impact
Discuss about the 2 `Specification` implementation seen (OO oriented, more FP oriented)
- Which one do you prefer and why?
- How would we want to use it on our production code?

Implementation solution available [here](solution.md)