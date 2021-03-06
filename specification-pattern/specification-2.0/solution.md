```c#
public static class DomainSpecifications
{
    public static ISpecification<Customer> IsAuthorizedInTheCountry(
        this ISpecification<Customer> specification,
        Movie movie) =>
        specification.And(customer => !movie.restrictedIn.Contains(customer.ResidenceCountry));

    public static ISpecification<Customer> HasTheRequiredAgeToWatch(this ISpecification<Customer> specification,
        Movie movie) => specification.And(customer => movie.MpaaRating != MpaaRating.NC17 || customer.Age > 17);
}

public class PlayMovieUseCase
{
    private readonly ISpecificationFactory _specificationFactory;

    public PlayMovieUseCase(ISpecificationFactory specificationFactory) => _specificationFactory = specificationFactory;

    public bool Handle(Customer customer, Movie movie)
        => _specificationFactory.Create<Customer>()
            .IsAuthorizedInTheCountry(movie)
            .HasTheRequiredAgeToWatch(movie)
            .IsSatisfiedBy(customer);
}
```   