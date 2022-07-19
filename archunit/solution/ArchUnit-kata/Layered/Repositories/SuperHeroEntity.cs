namespace ArchUnit.Kata.Layered.Repositories
{
    public record SuperHeroEntity(Guid Id, string Name, IReadOnlyList<string> Powers) {}
}