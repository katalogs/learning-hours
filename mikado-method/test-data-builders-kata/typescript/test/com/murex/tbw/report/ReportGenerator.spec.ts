import { ReportGenerator } from "../../../../../src/com/murex/tbw/report/ReportGenerator";
import { MainRepository } from "../../../../../src/com/murex/tbw/MainRepository";
import { InMemoryRepository } from "../storage/InMemoryRepository";
import { Author, EducationalBook, Category } from "../../../../../src/com/murex/tbw/domain/book";
import { Country, Currency, Language } from "../../../../../src/com/murex/tbw/domain/country";
import { Invoice, PurchasedBook } from "../../../../../src/com/murex/tbw/purchase";

describe(ReportGenerator, () => {
    it("Computes total amount without discount and without tax rate", () => {
      const repository = new InMemoryRepository();
      MainRepository.override(repository);
      const generator = new ReportGenerator();

      const book = new EducationalBook(
          "Clean Code", 25, new Author(
              "Uncle Bob", new Country(
                  "USA", Currency.Dollar, Language.English
              )
          ), Language.English, Category.COMPUTER);

      const purchase = new PurchasedBook(book, 2);

      const invoice = new Invoice("John Doe", new Country("USA", Currency.Dollar, Language.English));
      invoice.addPurchasedBook(purchase);

      repository.addInvoice(invoice);

      expect(generator.getTotalAmount()).toBe(57.5);
      expect(generator.getNumberOfIssuedInvoices()).toBe(1);
      expect(generator.getTotalSoldBooks()).toBe(2);

      MainRepository.reset();
    });
});
