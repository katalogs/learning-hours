using FluentAssertions;
using Xunit;

namespace Mutation.Tests
{
    public class DemoTests
    {
        [Fact]
        public void Should_Return_False_For_Abc() => Demo.IsLong("abc");
    }
}