using Xunit;
using static ArchUnit.Kata.Tests.ArchUnitExtensions;

namespace ArchUnit.Kata.Tests
{
    public class NamingConvention
    {
        [Fact]
        public void ServicesShouldBeSuffixedByService() =>
            EmptyRule("Each Service should be suffixed by Service")
                .Check();

        [Fact]
        public void CommandHandlersShouldBeSuffixedByCommandHandler() =>
            EmptyRule("Command handlers should be suffixed by CommandHandler")
                .Check();
    }
}