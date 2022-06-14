using System.Threading.Tasks;

namespace TestDoubles.Spy
{
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
}