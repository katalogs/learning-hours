using System.Threading.Tasks;
using FluentAssertions;
using VerifyXunit;
using Xunit;

namespace ApprovalTestingKata.Tests;

[UsesVerify]
public class OrderTests
{
    [Fact]
    public void New_order_should_have_correct_values()
    {
        var food = new Category(Name: "food", TaxPercentage: 10m);
        var salad = new Product("salad", 3.56m, food);
        var tomato = new Product("tomato", 4.65m, food);
        var order = new Order("EUR");

        order.AddProduct(2, salad);
        order.AddProduct(3, tomato);

        order.Status.Should().Be(OrderStatus.Created);
        order.Total.Should().Be(23.20m);
        order.Tax.Should().Be(2.13m);
        order.Currency.Should().Be("EUR");
        order.Items.Should().HaveCount(2);
        order.Items[0].Product.Name.Should().Be("salad");
        order.Items[0].Product.Price.Should().Be(3.56m);
        order.Items[0].Quantity.Should().Be(2);
        order.Items[0].TaxedAmount.Should().Be(7.84m);
        order.Items[0].Tax.Should().Be(0.72m);
        order.Items[1].Product.Name.Should().Be("tomato");
        order.Items[1].Product.Price.Should().Be(4.65m);
        order.Items[1].Quantity.Should().Be(3);
        order.Items[1].TaxedAmount.Should().Be(15.36m);
        order.Items[1].Tax.Should().Be(1.41m);
    }
}
