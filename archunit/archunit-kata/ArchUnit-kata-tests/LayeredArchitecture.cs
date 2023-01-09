using ArchUnitNET.Fluent.Syntax.Elements.Types;
using Xunit;
using static ArchUnit.Kata.Tests.ArchUnitExtensions;

namespace ArchUnit.Kata.Tests
{
    /// <summary>
    /// Detail your Layer Architecture here
    /// </summary>
    public class LayeredArchitecture
    {
        private static GivenTypesConjunctionWithDescription PresentationLayer() =>
            TypesInAssembly().And()
                .ResideInNamespace("Controllers")
                .As("Presentation Layer");

        private static GivenTypesConjunctionWithDescription ApplicationLayer() =>
            TypesInAssembly().And()
                .ResideInNamespace("Services")
                .As("Application Layer");

        private static GivenTypesConjunctionWithDescription DataAccessLayer() =>
            TypesInAssembly().And()
                .ResideInNamespace("Repositories")
                .As("Data Access Layer");

        private GivenTypesConjunctionWithDescription ModelLayer() =>
            TypesInAssembly().And()
                .ResideInNamespace("Models")
                .As("Model Layer");

        [Fact]
        public void PresentationRules() =>
            EmptyRule("Presentation Layer should not be accessed by another layer")
                .Check();

        [Fact]
        public void ApplicationRules() =>
            ApplicationLayer()
            .Should().NotDependOnAny(DataAccessLayer())
            .AndShould().NotDependOnAny(ModelLayer())
            .Because("Application Layer should depend on Presentation Layer")
            .Check();

        [Fact]
        public void DataAccessRules() =>
            EmptyRule("Data Access Layer should not depend on Application Layer nor Presentation Layer")
                .Check();

        [Fact]
        public void ModelRules() => 
            ModelLayer().Should()
            .NotDependOnAny(PresentationLayer())
            .AndShould().NotDependOnAny(DataAccessLayer())
            .AndShould().NotDependOnAny(ApplicationLayer())
            .Because("Model layer should not depend on other layers (DDD)")
            .Check();
    }
}