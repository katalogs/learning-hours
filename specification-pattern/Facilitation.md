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
public int xXx(DateTime postedAfter) => 
            xXx2(
                _blogRepository.FetchAll(),
                postedAfter
            ).Length;

private Post[] xXx2(Blogs.Blog[] blogs, DateTime postedAfter)
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

Code source is available in the `SpecificationDemo` folder

## Concepts
![Specification pattern explained](img/specification-pattern.png)

The specification pattern is a particular software design pattern, whereby business rules can be recombined by chaining the business rules together using boolean logic. 
The pattern is frequently used in the context of domain-driven design.

A `specification pattern outlines a business rule` that is combinable with other business rules. In this pattern, a unit of business logic inherits its functionality from the abstract aggregate Composite Specification class. 
The Composite Specification class has one function called `IsSatisfiedBy` that returns a boolean value. 

After instantiation, the specification is `chained` with other specifications, making new specifications easily maintainable, yet highly customizable business logic. 

As a consequence of performing runtime composition of high-level business/domain logic, the Specification pattern is a convenient tool for converting ad-hoc user search criteria into low level logic to be processed by repositories.

Since a specification is an `encapsulation of logic` in a `reusable form` it is very simple to thoroughly unit test, and when used in this context is also an implementation of the humble object pattern.

## Concrete Practice (35 min)
- Implement the missing `Specification(s)` to pass the tests

```text
Given a customer living in Russia and major When he/she wants to play a movie on Poutine Then he/she gets a rejection
Given a customer living in France and major When he/she wants to play a movie on Poutine Then he/she gets an approval
Given a minor customer When he/she wants to play an "olé olé" movie Then he/she gets a rejection
Given a major customer When he/she wants to play an "olé olé" movie Then he/she gets an approval
```
[![Olé olé](img/olé-olé.jpeg)](https://youtu.be/8bDmeeGVNvc)

## Conclusion (5min) - Impact
TODO
