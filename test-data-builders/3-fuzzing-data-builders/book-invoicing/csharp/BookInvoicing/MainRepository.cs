using BookInvoicing.Storage;

namespace BookInvoicing
{
    public static class MainRepository
    {
        public static IRepository ConfiguredRepository { get; private set; } = new JsonRepository();

        /**
         * Working effectively with Legacy Code
         * https://www.goodreads.com/book/show/44919.Working_Effectively_with_Legacy_Code
         */
        // TESTING ONLY
        public static void Override(IRepository newRepository)
        {
            ConfiguredRepository = newRepository;
        }

        // TESTING ONLY
        public static void Reset()
        {
            ConfiguredRepository = new JsonRepository();
        }
    }
}
