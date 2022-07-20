using System.Collections.Generic;
using BookInvoicing.Domain.Book;
using BookInvoicing.Purchase;

namespace BookInvoicing.Tests.Builders
{
    public class InvoiceBuilder
    {
        private string _customer;
        private CountryBuilder _country;
        private List<PurchasedBook> _books = new List<PurchasedBook>();

        private InvoiceBuilder(string customer, CountryBuilder country)
        {
            _customer = customer;
            _country = country;
        }

        public static InvoiceBuilder AnInvoice => new InvoiceBuilder("Customer", CountryBuilder.ACountry);

        public InvoiceBuilder ForCustomer(string name)
        {
            _customer = name;
            return this;
        }

        public InvoiceBuilder DeliveredTo(CountryBuilder country)
        {
            _country = country;
            return this;
        }

        public InvoiceBuilder Containing(int quantity, IBook book)
        {
            _books.Add(new PurchasedBook(book, quantity));
            return this;
        }

        public Invoice Build()
        {
            var invoice = new Invoice(_customer, _country.Build());
            invoice.AddPurchasedBooks(_books);
            return invoice;
        }
    }
}
