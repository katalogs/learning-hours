using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace TestDoubles;

public class Fake
{
    [Fact]
    public async Task Create_A_Joke_Use_Case_Should_Save_Good_Jokes()
    {
        var fakeJokeRepository = new FakeJokeRepository();
        var useCase = new CreateAJokeUseCase(fakeJokeRepository);
        var request = new CreateJokeRequest(
            "Anonymous",
            "Quelle partie du légume ne passe pas dans le mixer ? La chaise roulante"
        );

        await useCase.HandleAsync(request);

        fakeJokeRepository
            .ShouldContain(request);
    }
}

public class CreateAJokeUseCase
{
    private readonly IJokeRepository _jokeRepository;

    public CreateAJokeUseCase(IJokeRepository jokeRepository) => _jokeRepository = jokeRepository;


    public async Task HandleAsync(CreateJokeRequest createJokeRequest) =>
        await _jokeRepository
            .Save(ParseJoke(createJokeRequest));

    private static Joke ParseJoke(CreateJokeRequest createJokeRequest) =>
        new(createJokeRequest.Author, createJokeRequest.Text);
}

public interface IJokeRepository
{
    public Task Save(Joke joke);
}

public record Joke(string Author, string Text);

public record CreateJokeRequest(string Author, string Text);

public class FakeJokeRepository : IJokeRepository
{
    private readonly List<Joke> _jokes = new();

    public Task Save(Joke joke)
        => Task.Run(() => _jokes.Add(joke));

    public void ShouldContain(CreateJokeRequest request, int expectedCount = 1) =>
        _jokes
            .Where(j => j.Author == request.Author && j.Text == request.Text)
            .Should()
            .HaveCount(expectedCount);
}