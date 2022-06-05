using Specification;

namespace Movies;

public class AddToCartUseCase
{
    private readonly ISpecificationFactory _specificationFactory;

    public AddToCartUseCase(ISpecificationFactory specificationFactory)
    {
        _specificationFactory = specificationFactory;
    }

    public bool Handle(Customer customer, Movie movie) => throw new NotImplementedException();
}