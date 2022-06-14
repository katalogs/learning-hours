using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;

namespace TestDoubles.Spy
{
    public class SpyJokeRepository : IJokeRepository
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