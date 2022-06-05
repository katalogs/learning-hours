using Specification;

namespace Movies;

public class PlayMovieUseCase
{
    private readonly ISpecificationFactory _specificationFactory;

    public PlayMovieUseCase(ISpecificationFactory specificationFactory) => _specificationFactory = specificationFactory;

    public bool Handle(Customer customer, Movie movie)
        => throw new NotImplementedException();
}