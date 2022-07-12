namespace ArchUnit.Kata.Layered.Repositories
{
    public interface ISuperHeroRepository
    {
        Task<SuperHeroEntity> Save(SuperHeroEntity superHero);
    }
}