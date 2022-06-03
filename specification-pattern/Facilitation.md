# Specification Pattern
## Learning Goals
Understand how the Specification Pattern can help us:
- Write more readable code
- Reuse rules between different entities

## Connect - Rule comprehension
- Split the group in 2
  - In parallel groups have to identify clearly what are the rules implemented in their respected code snippet 
  - Give 1 minute to understand what is going on
  - Write in pure english

- Group 1 takes snippet 1 and group 2 takes snippet 2

`Snippet 1`
```c#
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
```

`Snippet 2`
```c#
public int xXx(DateTime postedAfter)
            => _blogRepository
                .FetchAll()
                .Where(blog => new ActivatedBlogSpecification().IsSatisfiedBy(blog))
                .SelectMany(blog => blog.Posts)
                .Count(post => new ActivatedSpecification()
                    .And(new NotRemovedSpecification())
                    .And(new NotBannedSpecification())
                    .And(new CreatedAfterSpecification(postedAfter)).IsSatisfiedBy(post));
```
   


## Concepts
- Specification Pattern -> IMAGE 
- Wikipedia
- In C#
  - Full Object
  - With `Builder` + `Extensions`

## Concrete Practice (35 min)
- Implement the `Specification` to pass the tests

```text
Je suis en Russie je suis majeur je veux acheter 1 film sur Poutine -> refus
Je suis en Russie je suis majeur je veux acheter 1 film sur Poutine -> accepté
Je suis mineur je veux acheter 1 film olé olé -> refus
Je suis majeur je veux acheter 1 film olé olé -> accepté

Reprendre les spécifications ici : https://github.com/bmgandre/dotnet-specification-pattern

## Conclusion (5min) - Impact
TODO
