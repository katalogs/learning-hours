using System.Collections.Generic;
using FluentAssertions;
using LanguageExt;
using Xunit;

namespace Mutation.Tests.Styles
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
            sut.Products
                .Should()
                .HaveCount(1)
                .And
                .Satisfy(item => item.Equals(product));
        }

        private sealed record Product(string Name);

        private class Order
        {
            private readonly List<Product> _products = new();

            public Seq<Product> Products => _products.ToSeq();

            public void Add(Product product)
            {
                _products.Add(product);
            }
        }
    }
}