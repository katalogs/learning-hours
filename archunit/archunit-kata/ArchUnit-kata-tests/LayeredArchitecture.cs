﻿using ArchUnitNET.Fluent.Syntax.Elements.Types;
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
            EmptyRule("Application Layer should depend on Presentation Layer")
                .Check();

        [Fact]
        public void DataAccessRules() =>
            EmptyRule("Data Access Layer should not depend on Application Layer nor Presentation Layer")
                .Check();

        [Fact]
        public void ModelRules() =>
            EmptyRule("Model Layer should not depend on any other layer")
                .Check();
    }
}