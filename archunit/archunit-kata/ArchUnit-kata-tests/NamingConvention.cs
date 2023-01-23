using ArchUnit.Kata.Examples;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;



namespace ArchUnit.Kata.Tests
{
    public class NamingConvention
    {
        [Fact]
        public void ServicesShouldBeSuffixedByService() =>
            Classes().That().ResideInNamespace("ArchUnit.Kata.Layered.Services").Should()
            .HaveNameEndingWith("Service")
            .Because("Each Service should be suffixed by Service")
            .Check();

        [Fact]
        public void CommandHandlersShouldBeSuffixedByCommandHandler() =>
            Classes().That().ImplementInterface(typeof(ICommandHandler<>)).Should()
            .HaveNameEndingWith("CommandHandler")
            .Because("Command handlers should be suffixed by CommandHandler")
            .Check();
    }
}