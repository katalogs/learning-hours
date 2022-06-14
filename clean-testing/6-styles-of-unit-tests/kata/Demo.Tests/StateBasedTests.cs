using Demo.Customer;
using FluentAssertions;
using Xunit;

namespace Demo.Tests
{

    public class StateBasedTests
    {
        [Fact]
        public void It_Should_Add_Given_Product_To_The_Order()
        {
            var product = new Product("Free Guy");
            var sut = new Order();

            sut.Add(product);

            // Verify the state
            sut.Products.Should()
                .HaveCount(1)
                .And.Satisfy(item => item.Equals(product));
        }
    }

}