using ArchUnit.Kata.Layered.Controllers;
using ArchUnit.Kata.Layered.Models;
using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Syntax.Elements.Types;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchUnit.Kata.Tests
{
    public static class ArchUnitExtensions
    {
        private static readonly Architecture Architecture =
            new ArchLoader()
                .LoadAssemblies(
                typeof(SuperHeroController).Assembly, 
                typeof(SuperHero).Assembly)
                .Build();

        public static GivenTypesConjunction TypesInAssembly()
            => Types().That().Are(Architecture.Types);

        public static IArchRule EmptyRule(string because)
            => Classes()
                .Should()
                .Be("Empty rule")
                .Because(because);

        public static void Check(this IArchRule rule) => rule.Check(Architecture);
    }
}