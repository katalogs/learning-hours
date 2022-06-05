using FluentAssertions;
using Specification;
using Xunit;
using static Movies.Country;
using static Movies.Tests.CustomerBuilder;
using static Movies.Tests.MovieBuilder;

namespace Movies.Tests;

public class ReadMovieUseCaseTests
{
    private readonly PlayMovieUseCase _useCase;

    public ReadMovieUseCaseTests() => _useCase = new PlayMovieUseCase(new SpecificationFactory());

    private static Movie APoutineMovie()
        => AMovie()
            .OnPoutine()
            .Build();

    private static Movie AnOléOléMovie()
        => AMovie()
            .OléOlé()
            .Build();

    [Fact(DisplayName =
        "Given a customer living in Russia When he/she want to play a movie on Poutine Then he/she gets a rejection")]
    public void ARussianResidentShouldNotBeAbleToPlayAMovieOnPoutine()
    {
        var customer = ANewCustomer()
            .LivingIn(Russia)
            .Build();

        _useCase.Handle(customer, APoutineMovie())
            .Should()
            .BeFalse();
    }


    [Fact(DisplayName =
        "Given a customer living in France When he/she wants to play a movie on Poutine Then he/she gets an approval")]
    public void AFranceResidentShouldBeAbleToPlayAMovieOnPoutine()
    {
        var customer = ANewCustomer()
            .LivingIn(France)
            .Major()
            .Build();

        _useCase.Handle(customer, APoutineMovie())
            .Should()
            .BeTrue();
    }

    [Fact(DisplayName =
        "Given a minor customer When he/she wants to play an olé olé movie Then he/she gets a rejection")]
    public void AMinorShouldNotBeAbleToPlayAMovieOléOlé()
    {
        var customer = ANewCustomer()
            .Minor()
            .Build();

        _useCase.Handle(customer, AnOléOléMovie())
            .Should()
            .BeFalse();
    }

    [Fact(DisplayName =
        "Given a major customer When he/she wants to play an olé olé movie Then he/she gets an approval")]
    public void AMajorShouldBeAbleToPlayAMovieOléOlé()
    {
        var customer = ANewCustomer()
            .Major()
            .Build();

        _useCase.Handle(customer, AnOléOléMovie())
            .Should()
            .BeTrue();
    }
}