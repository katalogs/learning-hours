using ArchUnitNET.Fluent.Syntax.Elements.Members.MethodMembers;
using Xunit;
using static ArchUnit.Kata.Tests.ArchUnitExtensions;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchUnit.Kata.Tests
{
    public class LinguisticAntiPatterns
    {
        private static GivenMethodMembersThat Methods() => MethodMembers().That().AreNoConstructors().And();

        [Fact]
        public void NoGetMethodShouldReturnVoid() =>
            EmptyRule("Getter should never return void")
                .Check();

        [Fact]
        public void IserAndHaserShouldReturnBooleans() =>
            EmptyRule("Is/Has methods should only return booleans")
                .Check();

        [Fact]
        public void SettersShouldNotReturnSomething() =>
            EmptyRule("Setters should not return something")
                .Check();
    }
}