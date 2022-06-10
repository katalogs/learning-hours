using FluentAssertions;
using Moq;
using Mutation.ToDo;
using Xunit;
using static LanguageExt.Seq;
using static Moq.MockBehavior;

namespace Mutation.Tests.ToDo
{
    public class TodoTests
    {
        [Fact]
        public void It_Should_Search_On_Repository_With_The_Given_Text()
        {
            var todoRepositoryMock = new Mock<ITodoRepository>(Strict);
            var todoService = new TodoService(todoRepositoryMock.Object);

            var searchResults = create(
                new Todo("Create miro", "add code samples in the board"),
                new Todo("Add myths in miro", "add mythbusters from ppt in the board")
            );

            const string searchedText = "miro";

            todoRepositoryMock
                .Setup(repo => repo.Search(searchedText))
                .Returns(searchResults);

            var results = todoService.Search(searchedText);

            results.Should().BeEquivalentTo(searchResults);
            todoRepositoryMock.Verify(repo => repo.Search(searchedText), Times.Once);
        }
    }
}