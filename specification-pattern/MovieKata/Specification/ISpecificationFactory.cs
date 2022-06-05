namespace Specification
{
    public interface ISpecificationFactory
    {
        ISpecification<T> Create<T>();
    }
}