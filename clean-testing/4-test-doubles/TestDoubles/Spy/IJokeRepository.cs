using System.Threading.Tasks;

namespace TestDoubles.Spy
{
    public interface IJokeRepository
    {
        public Task Save(Joke joke);
    }
}