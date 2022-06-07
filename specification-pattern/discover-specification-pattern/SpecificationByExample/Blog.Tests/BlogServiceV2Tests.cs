using System;
using FluentAssertions;
using Xunit;

namespace Blog.Tests
{
    public class BlogServiceV2Tests
    {
        private readonly BlogServiceV2 _blogService = new(new BlogRepositoryForTests());

        [Theory]
        [InlineData("1990-01-01", 3)]
        [InlineData("2020-01-01", 2)]
        [InlineData("2022-04-01", 1)]
        [InlineData("2022-10-01", 0)]
        public void CountAtDateShouldBeValid(DateTime postedAfter, int expectedCount) =>
            _blogService.CountValidPostsAfter(postedAfter)
                .Should()
                .Be(expectedCount);
    }
}