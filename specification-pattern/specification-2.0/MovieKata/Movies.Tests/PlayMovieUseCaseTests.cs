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

    private static Movie APutinMovie()
        => AMovie()
            .OnPutin()
            .Build();

    private static Movie AnOléOléMovie()
        => AMovie()
            .OléOlé()
            .Build();

    [Fact(DisplayName =
        "Given a customer living in Russia When they want to play a movie on Putin Then they gets a rejection")]
    public void ARussianResidentShouldNotBeAbleToPlayAMovieOnPutin()
    {
        var customer = ANewCustomer()
            .LivingIn(Russia)
            .Build();

        _useCase.Handle(customer, APutinMovie())
            .Should()
            .BeFalse();
    }


    [Fact(DisplayName =
        "Given a customer living in France When they wants to play a movie on Putin Then they get an approval")]
    public void AFranceResidentShouldBeAbleToPlayAMovieOnPutin()
    {
        var customer = ANewCustomer()
            .LivingIn(France)
            .Major()
            .Build();

        _useCase.Handle(customer, APutinMovie())
            .Should()
            .BeTrue();
    }

    [Fact(DisplayName =
        "Given a minor customer When they wants to play an olé olé movie Then they gets a rejection")]
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
        "Given a major customer When they wants to play an olé olé movie Then they gets an approval")]
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