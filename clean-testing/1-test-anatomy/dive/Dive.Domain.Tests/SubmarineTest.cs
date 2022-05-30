using System;
using FluentAssertions;
using Xunit;

namespace Dive.Domain.Tests
{
    public class SubmarineTest
    {
        [Fact]
        public void FirstTest()
        {
            true.Should()
                .BeFalse();
        }

        // Helper function to retrieve sample data for a complex scenario
        private string[] SubmarineCommands() => 
            typeof(SubmarineTest)
                .Assembly
                .GetResourceAsString("submarineCommands.txt")
                .Split(Environment.NewLine);
    }
}
