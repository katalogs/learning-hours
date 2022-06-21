using System.Collections.Generic;
using Anti_Patterns.Customer;
using FluentAssertions;
using Xunit;

namespace Anti_Patterns.Tests
{
    public class PriceEngineTests
    {
        [Fact]
        public void Discount_Of_3_Products_Should_Be_3_Percent()
        {
            var products = new List<Product> {new("P1"), new("P2"), new("P3")};
            var discount = PriceEngine.CalculateDiscount(products.ToArray());

            discount.Should()
                .Be(products.Count * 0.01);
        }
    }
}