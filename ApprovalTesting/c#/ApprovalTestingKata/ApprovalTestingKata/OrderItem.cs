namespace ApprovalTestingKata
{
    public record OrderItem(int Quantity, Product Product)
    {
        public decimal TaxedAmount => decimal.Round(Product.TaxedAmount * Quantity, 2, System.MidpointRounding.ToPositiveInfinity);
        public decimal Tax => decimal.Round(Product.Tax * Quantity, 2, System.MidpointRounding.ToPositiveInfinity);
    }
}
