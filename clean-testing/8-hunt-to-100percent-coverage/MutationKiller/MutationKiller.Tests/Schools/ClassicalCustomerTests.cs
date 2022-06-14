using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using FluentAssertions;
using Mutation.Customer;
using Xunit;

namespace Mutation.Tests.Schools
{
    public class ClassicalCustomerTests
    {
        private readonly Store _store;

        public ClassicalCustomerTests()
        {
            _store = new Store(
                new Dictionary<ProductType, int>
                        {{ProductType.Book, 10}}
                    .ToImmutableDictionary()
            );
        }

        [Fact]
        public void It_Should_Purchase_Successfully_When_Enough_Inventory()
        {
            var updatedStore = CustomerService.Purchase(_store, ProductType.Book, 6);

            updatedStore.IsSucc().Should().BeTrue();
            updatedStore
                .SuccessUnsafe()
                .GetInventoryFor(ProductType.Book)
                .Should()
                .Be(4);
        }

        [Fact]
        public void It_Should_Fail_When_Not_Enough_Inventory()
        {
            var updatedStore = CustomerService.Purchase(_store, ProductType.Book, 11);

            updatedStore.IsFail().Should().BeTrue();
            updatedStore
                .FailureUnsafe()
                .Should()
                .BeOfType<ArgumentException>();
        }
    }
}