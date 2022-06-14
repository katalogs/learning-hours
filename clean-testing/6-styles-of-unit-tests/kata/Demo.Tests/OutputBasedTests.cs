using Demo.Customer;
using FluentAssertions;
using Xunit;
using static Demo.Customer.PriceEngine;

namespace Demo.Tests
{

    public class OutputBasedTests
    {
        [Fact]
        public void Discount_Of_2_Products_Should_Be_2_Percent()
        {
            var product1 = new Product("Kaamelott");
            var product2 = new Product("Free Guy");

            // Call on the SUT (here PriceEngine)
            // No side effects -> Pure function
            var discount = CalculateDiscount(product1, product2);

            discount.Should().Be(0.02);
        }
    }

}