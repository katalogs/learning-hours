using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace TestDoubles
{
    public class Spy
    {
        [Fact]
        public async Task Create_A_Joke_Use_Case_Should_Save_Good_Jokes()
        {
            var spyJokeRepository = new SpyJokeRepository();
            var useCase = new CreateAJokeUseCase(spyJokeRepository);
            var request = new CreateJokeRequest(
                "Anonymous",
                "Quelle partie du légume ne passe pas dans le mixer ? La chaise roulante"
            );

            await useCase.HandleAsync(request);

            spyJokeRepository
                .ShouldContain(request);
        }

        private class CreateAJokeUseCase
        {
            private readonly IJokeRepository _jokeRepository;

            public CreateAJokeUseCase(IJokeRepository jokeRepository) => _jokeRepository = jokeRepository;

            public async Task HandleAsync(CreateJokeRequest createJokeRequest) =>
                await _jokeRepository
                    .Save(ParseJoke(createJokeRequest));

            private static Joke ParseJoke(CreateJokeRequest createJokeRequest) =>
                new(createJokeRequest.Author, createJokeRequest.Text);
        }

        private interface IJokeRepository
        {
            public Task Save(Joke joke);
        }

        private record Joke(string Author, string Text);

        private record CreateJokeRequest(string Author, string Text);

        private class SpyJokeRepository : IJokeRepository
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
    }
}