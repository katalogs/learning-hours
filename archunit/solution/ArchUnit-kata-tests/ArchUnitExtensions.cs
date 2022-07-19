using ArchUnit.Kata.Layered.Controllers;
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
                .LoadAssemblies(typeof(SuperHeroController).Assembly)
                .Build();
        
        public static GivenTypesConjunction TypesInAssembly() =>
            Types().That().Are(Architecture.Types);

        public static void Check(this IArchRule rule) => rule.Check(Architecture);
    }
}