using System;
using FluentAssertions;
using Mutation.Blog;
using Xunit;

namespace Mutation.Tests.Blog
{
    public class BlogShould
    {
        private const string Text = "Amazing article !!!";
        private const string Author = "Pablo Escobar";

        private readonly Article _article;

        protected BlogShould()
        {
            _article = new Article(
                "Lorem Ipsum",
                "consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore");
        }

        public class AddANewComment : BlogShould
        {
            [Fact]
            public void In_The_Article_Including_Given_Text_And_Author()
            {
                var updatedArticle = _article.AddComment(Text, Author);

                updatedArticle.IsRight.Should().BeTrue();
                AssertComment(updatedArticle.RightUnsafe().Comments.Head, Text, Author);
            }

            [Fact]
            public void In_An_Article_Containing_Existing_Ones()
            {
                const string newText = "Finibus Bonorum et Malorum";
                const string newAuthor = "Al Capone";

                var updatedArticle = _article.AddComment(Text, Author)
                    .Map(article => article.AddComment(newText, newAuthor))
                    .Flatten();

                updatedArticle.IsRight.Should().BeTrue();

                var comments = updatedArticle.RightUnsafe().Comments;
                comments.Should().HaveCount(2);
                AssertComment(comments.Last, newText, newAuthor);
            }

            private static void AssertComment(
                Comment comment,
                string expectedText,
                string expectedAuthor)
            {
                var (text, author, creationDate) = comment;
                text.Should().Be(expectedText);
                author.Should().Be(expectedAuthor);
                creationDate.DayNumber.Should().Be(DateOnly.FromDateTime(DateTime.Now).DayNumber);
            }
        }

        public class ReturnAValidationError : BlogShould
        {
            [Fact]
            void When_Adding_An_Existing_Comment()
            {
                var updatedArticle = _article.AddComment(Text, Author)
                    .Map(article => article.AddComment(Text, Author))
                    .Flatten();

                updatedArticle.IsLeft.Should().BeTrue();
                updatedArticle.LeftUnsafe()
                    .Should()
                    .HaveCount(1)
                    .And
                    .Satisfy(error => error.Description == "Comment already in the article");
            }
        }
    }
}