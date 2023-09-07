namespace ApprovalTestingKata
{
    public class Order
    {
        public string Currency { get; set; }
        public IList<OrderItem> Items { get; set; }

        public OrderStatus Status { get; set; }

        public Order(string currency)
        {
            Currency = currency;
            Status = OrderStatus.Created;
            Items = new List<OrderItem>();
        }

        public void AddProduct(int quantity, Product product)
        {
            Items.Add(new OrderItem(quantity, product));
        }

        public decimal Total => Items.Sum(item => item.TaxedAmount);
        public decimal Tax => Items.Sum(item => item.Tax);
    }
}
