using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using BookInvoicing.Domain.Book;
using BookInvoicing.Domain.Country;
using BookInvoicing.Purchase;
using static BookInvoicing.Domain.Book.Genre;
using static BookInvoicing.Domain.Country.Currency;

namespace BookInvoicing.Storage
{
    public sealed class JsonRepository : IRepository
    {
        public Dictionary<int, Invoice> Invoices { get; }

        public Dictionary<int, Invoice> GetInvoiceMap() => Invoices;

        public JsonRepository()
        {
            Invoices = new Dictionary<int, Invoice>();
            LoadJsonData();
        }

        private void LoadJsonData()
        {
            var jsonContent = GetJsonContent();
            var records = DeserializeJson(jsonContent);

            records.Select(CreateInvoice).ToList().ForEach(AddInvoice);
        }

        private string GetJsonContent()
        {
            try
            {
                return File.ReadAllText("./Storage/repository.json");
            }
            catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException)
            {
                Console.WriteLine();
                Console.WriteLine($"***** ERROR : JSON file was not found.");
                Console.WriteLine();
                Console.WriteLine();

                var emptyJson = "[]";
                return emptyJson;
            }
        }

        private Record[] DeserializeJson(string jsonContent)
        {
            return JsonSerializer.Deserialize<Record[]>(
                jsonContent,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = {new JsonStringEnumConverter()}
                });
        }

        private Invoice CreateInvoice(Record record)
        {
            var invoice = new Invoice(
                record.Id,
                record.Client,
                record.Country.ToDomain());

            var purchasedBooks = record.BooksInBasket
                .Select(b =>
                {
                    IBook book = b.Category.HasValue
                        ? new EducationalBook(b.Name, b.Price, b.Author.ToDomain(), b.Language,
                            b.Category.Value)
                        : (IBook) new Novel(b.Name, b.Price, b.Author.ToDomain(), b.Language, b._Genre);


                    return new PurchasedBook(book, b.Quantity);
                })
                .ToList();

            invoice.AddPurchasedBooks(purchasedBooks);
            return invoice;
        }

        public void AddInvoice(Invoice invoice)
        {
            Invoices.Add(invoice.Id, invoice);
        }


        private class Record
        {
            public int Id { get; set; }
            public string Client { get; set; }
            public Country Country { get; set; }
            public BookInBasket[] BooksInBasket { get; set; }
        }

        private class Country
        {
            public string Name { get; set; }
            public Language Language { get; set; }
            public string Currency { get; set; }

            public Currency _Currency => Convert.ToCurrency(Currency);

            public Domain.Country.Country ToDomain() => new Domain.Country.Country(Name, _Currency, Language);
        }

        private class BookInBasket
        {
            public string Name { get; set; }
            public float Price { get; set; }
            public int Quantity { get; set; }
            public Language Language { get; set; }
            public Category? Category { get; set; }
            public Author Author { get; set; }
            public string[] Genre { get; set; } = new string[0];

            public List<Genre> _Genre => Convert.ToGenre(Genre);
        }

        private class Author
        {
            public string Name { get; set; }
            public Nationality Nationality { get; set; }

            public Domain.Book.Author ToDomain() => new Domain.Book.Author(Name, Nationality.ToCountry());
        }

        private class Nationality
        {
            public string Name { get; set; }
            public Language Language { get; set; }
            public string Currency { get; set; }
            public Currency _Currency => Convert.ToCurrency(Currency);

            public Domain.Country.Country ToCountry() => new Domain.Country.Country(Name, _Currency, Language);
        }

        private class Convert
        {
            private static readonly Dictionary<string, Currency> CurrencyByName = new Dictionary<string, Currency>
            {
                {"US_DOLLAR", UsDollar},
                {"EURO", Euro},
                {"RENMINBI", Renminbi},
                {"POUND_STERLING", PoundSterling},
            };

            public static Currency ToCurrency(string currency) => CurrencyByName[currency];

            private static readonly Dictionary<string, Genre> Mapping = new Dictionary<string, Genre>
            {
                {"HORROR_FICTION", HorrorFiction},
                {"THRILLER", Thriller},
                {"DARK_FANTASY", DarkFantasy},
                {"MYSTERY", Mystery},
                {"ADVENTURE_FICTION", AdventureFiction},
                {"NON_FICTION", NonFiction},
                {"ROMANCE", Romance},
            };

            public static List<Genre> ToGenre(string[] g) => g.Select(g => Mapping[g]).ToList();
        }
    }
}
