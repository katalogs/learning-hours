using ArchUnit.Kata.Layered.Repositories;

namespace ArchUnit.Kata.Layered.Services
{
    public class SuperHeroService
    {
        private readonly ISuperHeroRepository superHeroRepository;

        public SuperHeroService(ISuperHeroRepository superHeroRepository)
        {
            this.superHeroRepository = superHeroRepository;
        }
    }
}