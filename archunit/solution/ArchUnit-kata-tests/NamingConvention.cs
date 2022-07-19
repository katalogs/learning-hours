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
                .HaveNameEndingWith("Service")
                .Check();

        [Fact]
        public void CommandHandlersShouldBeSuffixedByCommandHandler() =>
            Classes()
                .That()
                .ImplementInterface(typeof(ICommandHandler<>)).Should()
                .HaveNameEndingWith("CommandHandler")
                .Check();
    }
}

