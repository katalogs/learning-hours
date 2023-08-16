using System;
using ArchUnit.Kata.Examples;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchUnit.Kata.Tests
{
    public class NamingConvention
    {
        [Fact]
        public void ServicesShouldBeSuffixedByService() =>
            Classes()
                .That()
                .ResideInNamespace("Services").Should()
                .BeAssignableToTypesThat().Are(typeof(Exception)).AndShould()
                .HaveNameEndingWith("Service")
                .Check();
                
        [Fact]
        public void ServicesShouldBeMadeRedundant => {
            Classes().That().AreNotSealed().Should().NotBe().
        }

        [Fact]
        public void CommandHandlersShouldBeSuffixedByCommandHandler() =>
            Classes()
                .That()
                .ImplementInterface(typeof(ICommandHandler<>)).Should()
                .HaveNameEndingWith("CommandHandler")
                .Check();
    }
}

