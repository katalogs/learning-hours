using System.Collections.Generic;
using Anti_Patterns.ToDo;
using FluentAssertions;
using Moq;
using Xunit;
using static Moq.MockBehavior;

namespace Anti_Patterns.Tests
{
    public class TodoTests
    {
        [Fact]
        public void It_Should_Search_On_Repository_With_The_Given_Text()
        {
            var todoRepositoryMock = new Mock<ITodoRepository>(Strict);
            var todoService = new TodoService(todoRepositoryMock.Object);

            var searchResults = new List<Todo>
            {
                new("Create miro", "add code samples in the board"),
                new("Add myths in miro", "add mythbusters from ppt in the board")
            };

            const string searchedText = "miro";

            todoRepositoryMock
                .Setup(repo => repo.Search(searchedText))
                .Returns(searchResults);

            var results = todoRepositoryMock.Object.Search(searchedText);

            results.Should().BeEquivalentTo(searchResults);
            todoRepositoryMock.Verify(repo => repo.Search(searchedText), Times.Once);
        }
    }
}