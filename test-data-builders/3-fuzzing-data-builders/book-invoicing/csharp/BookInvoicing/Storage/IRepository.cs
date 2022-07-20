using System.Collections.Generic;
using BookInvoicing.Purchase;

namespace BookInvoicing.Storage
{
    public interface IRepository
    {
        void AddInvoice(Invoice invoice);

        Dictionary<int, Invoice> GetInvoiceMap();
    }
}
