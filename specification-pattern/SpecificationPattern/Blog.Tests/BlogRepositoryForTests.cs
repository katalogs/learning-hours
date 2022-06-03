using System;
using Blog.Posts;

namespace Blog.Tests
{
    public class BlogRepositoryForTests : IReadRepository<Blogs.Blog>
    {
        public Blogs.Blog[] FetchAll() =>
            new Blogs.Blog[]
            {
                new()
                {
                    Url = "https://www.hanselman.com/blog",
                    Activated = new DateTime(2008, 4, 4),
                    Created = new DateTime(2008, 4, 4),
                    Posts = new[]
                    {
                        new Post
                        {
                            Title = "Be a Better Developer in Six Months",
                            Activated = new DateTime(2008, 8, 4),
                            Created = new DateTime(2008, 8, 4)
                        },
                        new Post
                        {
                            Title = "How do you organize your code?",
                            Activated = new DateTime(2019, 3, 9),
                            Created = new DateTime(2022, 4, 9)
                        },
                        new Post
                        {
                            Title = "The Importance of being UTF-8",
                            Banned = DateTime.Now,
                            Activated = new DateTime(2019, 3, 9),
                            Created = new DateTime(2022, 4, 9)
                        },
                        new Post
                        {
                            Title = "The Importance of being UTF-8",
                            Removed = DateTime.Now,
                            Activated = DateTime.Now,
                            Created = new DateTime(2022, 4, 9)
                        },
                    }
                },
                new()
                {
                    Url = "https://www.reddit.com/",
                    Activated = new DateTime(2020, 1, 13),
                    Created = new DateTime(2020, 1, 13),
                    Posts = new[]
                    {
                        new Post
                        {
                            Title = "Putin about Ukraine",
                            Removed = new DateTime(2008, 8, 4),
                            Created = DateTime.Now,
                            Activated = DateTime.Now
                        },
                        new Post
                        {
                            Title = "Quand on veut on peut?",
                            Activated = new DateTime(2020, 3, 9),
                            Created = new DateTime(2020, 3, 9),
                        }
                    }
                }
            };
    }
}