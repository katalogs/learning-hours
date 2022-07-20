using BookInvoicing.Domain.Book;
using BookInvoicing.Purchase;

namespace BookInvoicing.Client
{
    public interface IOrder
    {
        void AddBook(IBook book, int quantity);

        Invoice CheckOut();

        int GetQuantityOf(IBook book);
    }
}
