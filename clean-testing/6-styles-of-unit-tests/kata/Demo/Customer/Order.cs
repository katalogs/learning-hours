namespace Demo.Customer
{

    public sealed class Order
    {
        private readonly List<Product> _products = new();

        public IEnumerable<Product> Products => _products;

        public void Add(Product product) => _products.Add(product);
    }

}