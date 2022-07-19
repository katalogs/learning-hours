using Xunit;
using static ArchUnit.Kata.Tests.ArchUnitExtensions;

namespace ArchUnit.Kata.Tests
{
    public class MethodReturnTypes
    {
        [Fact]
        public void CommandHandlersShouldOnlyReturnInt() =>
            EmptyRule("Command handler should only return ints")
                .Check();

        [Fact]
        public void ControllersPublicMethodShouldOnlyReturnApiResponse() =>
            EmptyRule("Controllers public methods should only return ApiResponse")
                .Check();
    }
}