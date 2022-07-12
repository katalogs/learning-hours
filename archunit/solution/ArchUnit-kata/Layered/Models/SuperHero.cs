namespace ArchUnit.Kata.Layered.Models
{
    public record SuperHero(Guid Id, string Name, IReadOnlyList<string> Powers) { }
}