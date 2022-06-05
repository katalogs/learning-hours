using FluentAssertions;
using Specification;
using Xunit;
using static Movies.Tests.CustomerBuilder;

namespace Movies.Tests;

public class CustomerTests
{
    private AddToCartUseCase _useCase;

    public CustomerTests()
    {
        _useCase = new AddToCartUseCase(new SpecificationFactory());
    }

    [Fact(DisplayName =
        "Etant donné que je suis en Russie et que je suis majeur Quand je veux acheter 1 film sur Poutine Alors j'obtiens 1 refus")]
    public void ARussianResidentShouldNotBeAbleToAddAMovieOnPoutine()
    {
        var customer = ARussianCustomer()
            .Major()
            .Build();

        var poutineMovie = MovieBuilder
            .AMovie()
            .OnPoutine()
            .Build();

        _useCase.Handle(customer, poutineMovie)
            .Should()
            .BeFalse();
    }
}