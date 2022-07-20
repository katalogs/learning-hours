using BookInvoicing.Domain.Book;

namespace BookInvoicing.Tests.Builders
{
    public class AuthorBuilder
    {
        private string _name;
        private CountryBuilder _country;

        private AuthorBuilder(string name, CountryBuilder country)
        {
            _name = name;
            _country = country;
        }

        public static AuthorBuilder AnAuthor => new AuthorBuilder("Name", CountryBuilder.ACountry);
        public static AuthorBuilder UncleBob => AnAuthor.Named("Uncle Bob").LivingIn(CountryBuilder.Usa);

        public AuthorBuilder Named(string name)
        {
            _name = name;
            return this;
        }

        public AuthorBuilder LivingIn(CountryBuilder country)
        {
            _country = country;
            return this;
        }

        public Author Build()
        {
            return new Author(_name, _country.Build());
        }
    }
}
