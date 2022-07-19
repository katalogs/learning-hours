using ArchUnit.Kata.Layered.Repositories;
using ArchUnit.Kata.Layered.Services;

namespace ArchUnit.Kata.Layered.Controllers
{
    public class SuperHeroController
    {
        private readonly SuperHeroService superHeroService;
        private readonly ISuperHeroRepository superHeroRepository;

        public SuperHeroController(SuperHeroService superHeroService,
            ISuperHeroRepository superHeroRepository)
        {
            this.superHeroService = superHeroService;
            this.superHeroRepository = superHeroRepository;
        }
    }
}