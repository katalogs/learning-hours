using System.Diagnostics.CodeAnalysis;
using ArchUnit.Kata.Tests;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchUnit.Kata.Tests
{

    public class Annotations
    {
        [Fact]
        public void CoverageAttributesShouldNotBeUsedOnClasses() =>
            Classes().That().HaveAnyAttributes(typeof(ExcludeFromCodeCoverageAttribute)).Should()
                .NotExist()
                .Because("You should use config file instead of ExcludeFromCodeCoverageAttribute")
                .Check();

        [Fact]
        public void CoverageAttributesShouldNotBeUsedOnMethods() =>
            MethodMembers().That().HaveAnyAttributes(typeof(ExcludeFromCodeCoverageAttribute)).Should()
                .NotExist()
                .Because("You should use config file instead of ExcludeFromCodeCoverageAttribute")
                .Check();

        [Fact]
        public void CoverageAttributesShouldNotBeUsedOnProperties() =>
            PropertyMembers().That().HaveAnyAttributes(typeof(ExcludeFromCodeCoverageAttribute)).Should()
                .NotExist()
                .Because("You should use config file instead of ExcludeFromCodeCoverageAttribute")
                .Check();
    }
}