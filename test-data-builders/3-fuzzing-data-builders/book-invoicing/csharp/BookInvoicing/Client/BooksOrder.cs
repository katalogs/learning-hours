using System.Collections.Generic;
using BookInvoicing.Domain.Book;
using BookInvoicing.Purchase;

namespace BookInvoicing.Client
{
   public sealed class BooksOrder : IOrder
    {
        private readonly Dictionary<IBook, int> _booksInBasket;
        private readonly Client _client;

        public BooksOrder(Client client)
        {
            _client = client;
            _booksInBasket = new Dictionary<IBook, int>();
        }

        public void AddBook(IBook book, int quantity)
        {
            var existingQuanty =  _booksInBasket.GetValueOrDefault(book,0);
            _booksInBasket[book] = existingQuanty + quantity;
        }

        public Invoice CheckOut()
        {
            var invoice = new Invoice(_client.Name, _client.Country);
            
            foreach (var (book, quantity) in _booksInBasket)
                invoice.AddPurchasedBook(new PurchasedBook(book, quantity));

            MainRepository.ConfiguredRepository.AddInvoice(invoice);
            return invoice;
        }

        public int GetQuantityOf(IBook book) => _booksInBasket.GetValueOrDefault(book, 0);
    }
}
