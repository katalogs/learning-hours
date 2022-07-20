using BookInvoicing.Domain.Country;

namespace BookInvoicing.Domain.Book
{
    public interface IBook
    {
        string Name { get; }
        double Price { get; }
        Author Author { get; }
        Language Language { get; }
    }
}
