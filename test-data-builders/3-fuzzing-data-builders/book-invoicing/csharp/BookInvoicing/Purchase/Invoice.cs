using System;
using System.Collections.Generic;
using System.Linq;
using BookInvoicing.Domain.Country;
using BookInvoicing.Finance;

namespace BookInvoicing.Purchase
{
    public sealed class Invoice
    {
        public int Id { get; }
        public string ClientName { get; }
        public Country Country { get; }
        public List<PurchasedBook> PurchasedBooks { get; }


        public Invoice(string clientName, Country country)
            : this(IdGenerator.NextId(), clientName, country)
        {
        }

        public Invoice(int id, string clientName, Country country)
        {
            Id = id;
            ClientName = clientName;
            Country = country;
            PurchasedBooks = new List<PurchasedBook>();
        }

        public void AddPurchasedBooks(List<PurchasedBook> books)
        {
            PurchasedBooks.AddRange(books);
        }

        public void AddPurchasedBook(PurchasedBook book)
        {
            PurchasedBooks.Add(book);
        }

        public double ComputeTotalAmount()
        {
            return PurchasedBooks.Sum(book => book.TotalPrice * TaxRule.GetApplicableRate(Country, book.Book));
        }

        public override string ToString() => $"Invoice [ {nameof(Id)}: '{Id}'" +
                                             $", {nameof(ClientName)}: '{ClientName}'" +
                                             $", {nameof(Country)}: '{Country}'" +
                                             $", {nameof(PurchasedBooks)}: '{PurchasedBooks}' ]";

        private bool Equals(Invoice other) => Id == other.Id
                                              && ClientName == other.ClientName
                                              && Equals(Country, other.Country)
                                              && Equals(PurchasedBooks, other.PurchasedBooks);


        public override bool Equals(object obj) => ReferenceEquals(this, obj)
                                                   || obj is Invoice other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Id, ClientName, Country, PurchasedBooks);
    }
}
