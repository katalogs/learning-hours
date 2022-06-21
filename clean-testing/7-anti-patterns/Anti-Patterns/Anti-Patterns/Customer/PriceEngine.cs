namespace Anti_Patterns.Customer
{
    public static class PriceEngine
    {
        public static double CalculateDiscount(params Product[] products) 
            => Math.Min(products.Length * 0.01, 0.2);
    }
}