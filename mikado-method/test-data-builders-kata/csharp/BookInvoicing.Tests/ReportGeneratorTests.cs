using System.Collections.Generic;
using BookInvoicing.Domain.Book;
using BookInvoicing.Domain.Country;
using BookInvoicing.Purchase;
using BookInvoicing.Report;
using BookInvoicing.Tests.Storage;
using Xunit;

namespace BookInvoicing.Tests
{
    public class ReportGeneratorTests
    {
        [Fact]
        public void ShouldComputeTotalAmount_WithoutDiscount_WithoutTaxExchange()
        {
            // Arrange
            var inMemoryRepository = OverrideRepositoryForTests();
            ReportGenerator generator = new ReportGenerator();

            var book = new EducationalBook(
                "Clean Code", 25, new Author(
                    "Uncle Bob", new Country(
                        "USA", Currency.UsDollar, Language.English
                        )
                    ),
                Language.English, Category.Computer
            );

            var purchasedBook = new PurchasedBook(book, 2);

            Invoice invoice = new Invoice("John Doe", new Country("USA", Currency.UsDollar, Language.English));
            invoice.AddPurchasedBooks(new List<PurchasedBook> { purchasedBook });

            // Act
            inMemoryRepository.AddInvoice(invoice);

            // Assert
            Assert.Equal(50, generator.GetTotalAmount());
            Assert.Equal(1, generator.GetNumberOfIssuedInvoices());
            Assert.Equal(2, generator.GetTotalSoldBooks());

            ResetTestsRepository();
        }

        [Fact]
        public void ShouldComputeTotalAmount_WithDiscount_WithTaxExchanges()
        {
            // Arrange
            var inMemoryRepository = OverrideRepositoryForTests();
            ReportGenerator generator = new ReportGenerator();

            var book = new Novel("A mysterious adventure fiction", 35.5, new Author(
                    "Some Guy", new Country(
                        "France", Currency.Euro, Language.French
                        )
                    ),
                Language.English, new List<Genre> {Genre.Mystery, Genre.AdventureFiction }
            );

            var purchasedBook = new PurchasedBook(book, 3);

            var invoice = new Invoice("John Doe", new Country(
                "Germany", Currency.Euro, Language.German
            ));
            invoice.AddPurchasedBooks(new List<PurchasedBook> { purchasedBook });

            // Act
            inMemoryRepository.AddInvoice(invoice);

            // Assert
            Assert.Equal(106.5, generator.GetTotalAmount());
            Assert.Equal(1, generator.GetNumberOfIssuedInvoices());
            Assert.Equal(3, generator.GetTotalSoldBooks());

            ResetTestsRepository();
        }

        private InMemoryRepository OverrideRepositoryForTests()
        {
            InMemoryRepository inMemoryRepository = new InMemoryRepository();
            MainRepository.Override(inMemoryRepository);
            return inMemoryRepository;
        }

        private void ResetTestsRepository()
        {
            MainRepository.Reset();
        }
    }
}
