using BookInvoicing.Domain.Country;

namespace BookInvoicing.Tests.Builders
{
    public class CountryBuilder
    {
        private string _name;
        private Language _language;
        private Currency _currency;

        private CountryBuilder(string name, Language language, Currency currency)
        {
            _name = name;
            _language = language;
            _currency = currency;
        }

        public static CountryBuilder ACountry => new CountryBuilder("Country", Language.English, Currency.UsDollar);

        public static CountryBuilder Usa => new CountryBuilder("USA", Language.English, Currency.UsDollar);

        public CountryBuilder Named(string name)
        {
            _name = name;
            return this;
        }

        public CountryBuilder Speaking(Language language)
        {
            _language = language;
            return this;
        }

        public CountryBuilder PayingIn(Currency currency)
        {
            _currency = currency;
            return this;
        }

        public Country Build()
        {
            return new Country(_name, _currency, _language);
        }

        public CountryBuilder But()
        {
            return new CountryBuilder(_name, _language, _currency);
        }
    }
}
