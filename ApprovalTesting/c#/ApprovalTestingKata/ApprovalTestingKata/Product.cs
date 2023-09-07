namespace ApprovalTestingKata
{
    public record Product(string Name, decimal Price, Category Category)
    {
        public decimal TaxedAmount => Price + Tax;
        public decimal Tax => decimal.Round((Price / 100) * Category.TaxPercentage,2, System.MidpointRounding.ToPositiveInfinity);
    }
}
