namespace Specification
{
    public static class SpecificationQueryableExtensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> q, ISpecification<T> spec)
        {
            return q.Where(spec.Predicate);
        }
    }
}