using Xunit;
using static ArchUnitNET.Fluent.Slices.SliceRuleDefinition;
using static ArchUnit.Kata.Tests.ArchUnitExtensions;

namespace ArchUnit.Kata.Tests
{
    public class Guidelines
    {
        [Fact]
        public void ClassShouldNotDependOnAnother() =>
            EmptyRule("Class with name SomeExample should not depend on Other")
                .Check();

        [Fact]
        public void AnnotatedClassesShouldResideInAGivenNamespace() =>
            EmptyRule("Class annotated with ApiController should be under Controllers")
                .Check();

        [Fact]
        public void ServiceSliceHasNoCycleDependencies() =>
            Slices()
                .Matching("*.Modules.(*)").Should()
                .BeFreeOfCycles()
                .Check();

        [Fact]
        public void InterfacesShouldStartWithI() =>
            EmptyRule("Every interface should start with a big I")
                .Check();

        [Fact]
        public void ClassesInDomainCanOnlyAccessClassesInDomainItself() =>
            EmptyRule("Classes in the Domain should only depend on Types in Domain itself")
                .Check();
    }
}