using Specification;

namespace Movies
{
    public static class SpecificationExtension
    {
        public static ISpecification<Customer> IsMovieAuthorized(this ISpecification<Customer> CustomerSpecification, Movie movie)
        => CustomerSpecification.IsCountryAuthorized(movie)
                                .IsAgeAuthorized(movie);

        private static ISpecification<Customer> IsCountryAuthorized(this ISpecification<Customer> CustomerSpecification, Movie movie)
            => CustomerSpecification.And(customer => !movie.RestrictedIn.Contains(customer.ResidenceCountry));


        private static ISpecification<Customer> IsAgeAuthorized(this ISpecification<Customer> CustomerSpecification, Movie movie)
          => CustomerSpecification.And(customer => movie.MpaaRating != MpaaRating.NC17 || customer.Age > 16);

    }
}
