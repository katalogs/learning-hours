We use `moq` in this solution
 
## Dummy
No need to use a library for Dummy stuff

## Fake
Regarding the `fake`, its idea is to have a concrete implementation so no impact with a library

## Stub
- Here we have implemented 2 classes to support `Authorize()` results : `AllowAccessAuthorizer`,  `DenyAccessAuthorizer`
	- We can delete them
	- And replace them by setup our `mock` behavior on this method call

```c#
public class StubTests
{
    private readonly Mock<IAuthorizer> _authorizerStub = new();

    [Fact]
    public void Should_Divide_A_Numerator_By_A_Denominator_When_Authorization_Is_Accepted()
    {
        _authorizerStub.Setup(_ => _.Authorize()).Returns(true);
        var calculator = new Calculator(_authorizerStub.Object);

        calculator.Divide(9, 3)
            .Should()
            .Be(3);
    }

    [Fact]
    public void Should_Divide_A_Numerator_By_A_Denominator_When_Authorization_Is_Denied()
    {
        _authorizerStub.Setup(_ => _.Authorize()).Returns(false);
        var calculator = new Calculator(_authorizerStub.Object);

        calculator.Invoking(_ => _.Divide(9, 3))
            .Should()
            .Throw<UnauthorizedAccessException>();
    }
}
```

## Spy
- We can delete our `Spy` implementation : `SpyJokeRepository`
	- We replace by a `Mock` instantiation and a call on `Verify`
	- In `moq` we can `Verify` a lot of stuff : the of times a given method is called for example

```c#
[Fact]
public async Task Create_A_Joke_Use_Case_Should_Save_Good_Jokes()
{
    var spyJokeRepository = new Mock<IJokeRepository>();
    var useCase = new CreateAJokeUseCase(spyJokeRepository.Object);
    var request = new CreateJokeRequest(
        "Anonymous",
        "Quelle partie du légume ne passe pas dans le mixer ? La chaise roulante"
    );

    await useCase.HandleAsync(request);

    spyJokeRepository
        .Verify(_ => _.Save(new Joke(request.Author, request.Text)), Times.Once);
}
```

## Mock
- We can delete our `Mock` implementation : `MockNotifier`
	- We replace by a `Mock` instantiation and a call on `Verify`

```c#
[Fact]
public void Should_Notify_Twice_When_Receiving_A_Scenario_And_Having_Two_Clients()
{
    var clients = new List<Client>
    {
        new("Cliff Booth", "cliff.booth@double.com"),
        new("Rick Dalton", "rick.dalton@double.com")
    };

    var notifierMock = new Mock<INotifier>();
    var scriptEventHandler = new ScenarioReceivedEventHandler(notifierMock.Object, clients);
    var @event = new ScenarioReceived("The 14 fists of McCluskey");

    scriptEventHandler.Handle(@event);

    clients.ForEach(client =>
        notifierMock.Verify(_ => _.Notify(client, @event), Times.Once)
    );
}
```