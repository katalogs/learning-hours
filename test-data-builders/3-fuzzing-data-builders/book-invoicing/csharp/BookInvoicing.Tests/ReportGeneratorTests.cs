using System;
using BookInvoicing.Domain.Country;
using BookInvoicing.Report;
using BookInvoicing.Tests.Storage;
using Xunit;
using static BookInvoicing.Tests.Builders.BookBuilder;
using static BookInvoicing.Tests.Builders.CountryBuilder;
using static BookInvoicing.Tests.Builders.InvoiceBuilder;


namespace BookInvoicing.Tests
{
    public class ReportGeneratorTests: IDisposable
    {
        private readonly InMemoryRepository _inMemoryRepository;
        private ReportGenerator _generator => new ReportGenerator();

        public ReportGeneratorTests()
        {
            _inMemoryRepository = OverrideRepositoryForTests();
        }

        [Fact]
        public void ShouldComputeTotalAmount_WithoutConversionRate_WithoutTaxRule()
        {
            // Arrange
            var book = AnEducationalBook
                .Costing(25)
                .Build();

            var invoice = AnInvoice
                .DeliveredTo(ACountry.Named("Spain").PayingIn(Currency.UsDollar))
                .Containing(2, book)
                .Build();

            // Act
            _inMemoryRepository.AddInvoice(invoice);

            // Assert
            Assert.Equal(50, _generator.GetTotalAmount());
        }

        [Fact]
        public void ShouldComputeTotalAmount_WithConversionRate_WithoutTaxRule()
        {
            // Arrange
            var book = AnEducationalBook
                .WrittenIn(Language.Spanish)
                .Costing(30.5)
                .Build();

            var invoice = AnInvoice
                .DeliveredTo(ACountry.Named("Spain").PayingIn(Currency.Euro))
                .Containing(3, book)
                .Build();

            // Act
            _inMemoryRepository.AddInvoice(invoice);

            // Assert
            Assert.Equal(114.74, _generator.GetTotalAmount());
        }

        [Fact]
        public void ShouldComputeTotalAmount_WithoutConversionRate_WithTaxRule()
        {
            // Arrange
            var book = AnEducationalBook
                .Costing(25)
                .Build();

            var invoice = AnInvoice
                .DeliveredTo(Usa)
                .Containing(2, book)
                .Build();

            // Act
            _inMemoryRepository.AddInvoice(invoice);

            // Assert
            Assert.Equal(57.5, _generator.GetTotalAmount());
        }

        [Fact]
        public void ShouldComputeTotalAmount_WithConversionRate_WithTaxRule()
        {
            // Arrange
            var book = AnEducationalBook
                .Costing(25)
                .Build();

            var invoice = AnInvoice
                .DeliveredTo(ACountry.Named("France").PayingIn(Currency.Euro))
                .Containing(2, book)
                .Build();

            // Act
            _inMemoryRepository.AddInvoice(invoice);

            // Assert
            Assert.Equal(71.25, _generator.GetTotalAmount());
        }

        private InMemoryRepository OverrideRepositoryForTests()
        {
            InMemoryRepository inMemoryRepository = new InMemoryRepository();
            MainRepository.Override(inMemoryRepository);
            return inMemoryRepository;
        }

        private void ResetTestsRepository() => MainRepository.Reset();

        public void Dispose() => ResetTestsRepository();
    }


}
