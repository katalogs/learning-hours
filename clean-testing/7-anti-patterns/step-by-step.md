# Step-by-step
### Leaking algorithm implementation in tests
We remove the leak and `hardcode` expectedResult

```c#
public class PriceEngineTests
{
    [Fact]
    public void Discount_Of_3_Products_Should_Be_3_Percent()
    {
        var products = new List<Product> {new("P1"), new("P2"), new("P3")};
        var discount = PriceEngine.CalculateDiscount(products.ToArray());

        discount.Should()
            .Be(0.03);
    }
}
```

### Test external code/lib

Use the `todoService` as SUT

```c#
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

    var results = todoService.Search(searchedText);

    todoRepositoryMock.Verify(repo => repo.Search(searchedText), Times.Once);
    results.Should().BeEquivalentTo(searchResults);
}
```

### Duplication everywhere

- Centralize constants
- Instantiate a default `Article`
  - Use DataBuilders if necessary

```c#
public class BlogTests
{
    private const string Text = "Amazing article !!!";
    private const string Author = "Pablo Escobar";

    private readonly Article _article = new(
        "Lorem Ipsum",
        "consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore"
    );


    [Fact]
    public void It_Should_Return_a_Right_For_Valid_Comment()
    {
        var result = _article.AddComment(Text, Author);
        result.IsRight.Should().BeTrue();
    }

    [Fact]
    public void It_Should_Add_A_Comment_With_The_Given_Text()
    {
        var result = _article.AddComment(Text, Author);

        result.Should()
            .BeRight(right => right.Comments
                .Should()
                .HaveCount(1).And
                .Satisfy(comment => comment.Text == Text)
            );
    }

    [Fact]
    public void It_Should_Add_A_Comment_With_The_Given_Author()
    {
        var result = _article.AddComment(Text, Author);

        result.Should()
            .BeRight(right => right.Comments
                .Should()
                .HaveCount(1).And
                .Satisfy(comment => comment.Author == Author)
            );
    }

    [Fact]
    public void It_Should_Add_A_Comment_With_The_Date_Of_The_Day()
    {
        var article = _article;
        var result = article.AddComment(Text, Author);
    }

    [Fact]
    public void It_Should_Add_A_Left_When_Adding_Existing_Comment()
    {
        var result = _article.AddComment(Text, Author)
            .Map(a => a.AddComment(Text, Author))
            .Flatten();

        result.IsLeft.Should().BeTrue();
    }
}
```

### Only 1 assert per test
- Split Test Cases : passing / failure
  - Use inheritance
- Assert all the outputs in the same test
  - Comments / Text / Author / CreationDate

```c#
public class BlogShould
{
    private const string Text = "Amazing article !!!";
    private const string Author = "Pablo Escobar";

    private readonly Article _article = new(
        "Lorem Ipsum",
        "consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore"
    );

    public class AddANewComment : BlogShould
    {
        [Fact]
        public void It_Should_Return_a_Right_For_Valid_Comment()
        {
            var result = _article.AddComment(Text, Author);

            result.Should()
                .BeRight(right => right.Comments
                    .Should()
                    .HaveCount(1).And
                    .Satisfy(comment => comment.Text == Text)
                    .And
                    .Satisfy(comment => comment.Author == Author)
                    .And
                    .Satisfy(comment =>
                        comment.CreationDate.DayNumber == DateOnly.FromDateTime(DateTime.Now).DayNumber)
                );
        }
    }

    public class ReturnAValidationError : BlogShould
    {
        [Fact]
        public void It_Should_Add_A_Left_When_Adding_Existing_Comment()
        {
            var result = _article.AddComment(Text, Author)
                .Map(a => a.AddComment(Text, Author))
                .Flatten();

            result.IsLeft.Should().BeTrue();
        }
    }
}
```

### Technical concepts in test names
- Improve naming to remove technical concepts (Left / Right)
- Improve `Error` assertions to better fit to the test name

```c#
public class BlogShould
{
    private const string Text = "Amazing article !!!";
    private const string Author = "Pablo Escobar";

    private readonly Article _article = new(
        "Lorem Ipsum",
        "consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore"
    );

    public class AddANewComment : BlogShould
    {
        [Fact]
        public void In_The_Article_Including_Given_Text_And_Author()
        {
            var result = _article.AddComment(Text, Author);

            result.Should()
                .BeRight(right => right.Comments
                    .Should()
                    .HaveCount(1).And
                    .Satisfy(comment => comment.Text == Text)
                    .And
                    .Satisfy(comment => comment.Author == Author)
                    .And
                    .Satisfy(comment =>
                        comment.CreationDate.DayNumber == DateOnly.FromDateTime(DateTime.Now).DayNumber)
                );
        }
    }

    public class ReturnAValidationError : BlogShould
    {
        [Fact]
        public void When_Adding_An_Existing_Comment()
        {
            var updatedArticle = _article.AddComment(Text, Author)
                .Map(a => a.AddComment(Text, Author))
                .Flatten();

            updatedArticle.Should()
                .BeLeft(error =>
                    error.Should().HaveCount(1)
                        .And
                        .Satisfy(_ => _.Description == "Comment already in the article"));
        }
    }
}
```

### Ambiguous naming
Solved by previous refactoring

### Missing/shitty assertions
We added the assertion on the date in the previous steps

### Still some improvements
- By clarifying our test cases we identified a test case is missing
  - Not easy to spot when there is no test quality
  - Indeed, we should add a test case to check the behavior when we add a comment on an article containing comments already
- Avoid generic names like `result`, `context`, ...
- Be careful with `Non-deterministic data` like `Date`, `Guid`, `Random`, ...
  - Handle those kind of stuff through `Objects`, `Higher Order Functions`, ...

```c#
public class AddANewComment : BlogShould
{
    [Fact]
    public void In_The_Article_Including_Given_Text_And_Author() =>
        _article.AddComment(Text, Author)
            .Should()
            .BeRight(right =>
            {
                right.Comments.Should().HaveCount(1);
                AssertComment(right.Comments.Last(), Text, Author);
            });

    [Fact]
    public void In_An_Article_Containing_Existing_Ones()
    {
        const string newText = "Finibus Bonorum et Malorum";
        const string newAuthor = "Al Capone";

        var updatedArticle = _article.AddComment(Text, Author)
            .Map(article => article.AddComment(newText, newAuthor))
            .Flatten();

        updatedArticle.Should()
            .BeRight(right =>
            {
                right.Comments.Should().HaveCount(2);
                AssertComment(right.Comments.Last(),
                    newText,
                    newAuthor);
            });
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
```

We have centralized `Comment` assertion code as well

We `refactor` again and here is the final result:
```c#
public class BlogShould
{
    private const string Text = "Amazing article !!!";
    private const string Author = "Pablo Escobar";

    private readonly Article _article = new(
        "Lorem Ipsum",
        "consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore"
    );

    public class AddANewComment : BlogShould
    {
        [Fact]
        public void In_The_Article_Including_Given_Text_And_Author() =>
            _article.AddComment(Text, Author)
                .Should()
                .BeRight(right =>
                {
                    right.Comments.Should().HaveCount(1);
                    AssertComment(right.Comments.Last(), Text, Author);
                });

        [Fact]
        public void In_An_Article_Containing_Existing_Ones()
        {
            const string newText = "Finibus Bonorum et Malorum";
            const string newAuthor = "Al Capone";

            _article.AddComment(Text, Author)
                .Map(article => article.AddComment(newText, newAuthor))
                .Flatten().Should()
                .BeRight(right =>
                {
                    right.Comments.Should().HaveCount(2);
                    AssertComment(right.Comments.Last(),
                        newText,
                        newAuthor);
                });
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
        public void When_Adding_An_Existing_Comment() =>
            AddSameCommentTwice()
                .Should()
                .BeLeft(error =>
                    error.Should().HaveCount(1)
                        .And
                        .Satisfy(_ => _.Description == "Comment already in the article"));

        private Either<IEnumerable<ValidationError>, Article> AddSameCommentTwice() =>
            _article.AddComment(Text, Author)
                .Map(a => a.AddComment(Text, Author))
                .Flatten();
    }
}
```