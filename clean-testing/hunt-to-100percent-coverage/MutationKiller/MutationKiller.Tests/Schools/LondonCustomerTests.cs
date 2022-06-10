using System;
using FluentAssertions;
using Moq;
using Mutation.Customer;
using Xunit;
using static Mutation.Customer.ProductType;

namespace Mutation.Tests.Schools
{
    public class LondonCustomerTests
    {
        private const int Quantity = 6;
        private readonly Mock<IStore> _storeMock;

        public LondonCustomerTests()
        {
            _storeMock = new Mock<IStore>();
        }

        [Fact]
        public void It_Should_Purchase_Successfully_When_Enough_Inventory()
        {
            _storeMock
                .Setup(s => s.HasEnoughInventory(Book, Quantity))
                .Returns(true);

            var updatedStore = CustomerService.Purchase(_storeMock.Object, Book, 6);

            updatedStore.IsSucc().Should().BeTrue();
            _storeMock.Verify(s => s.RemoveInventory(Book, Quantity), Times.Once);
        }

        [Fact]
        public void It_Should_Fail_When_Not_Enough_Inventory()
        {
            _storeMock
                .Setup(s => s.HasEnoughInventory(Book, Quantity))
                .Returns(false);

            var updatedStore = CustomerService.Purchase(_storeMock.Object, Book, 11);

            updatedStore.IsFail().Should().BeTrue();
            updatedStore
                .FailureUnsafe()
                .Should()
                .BeOfType<ArgumentException>();
            
            _storeMock.Verify(s => s. RemoveInventory(Book, Quantity), Times.Never);
        }
    }
}