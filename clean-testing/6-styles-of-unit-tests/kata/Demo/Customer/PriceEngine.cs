namespace Demo.Customer
{

    public static class PriceEngine
    {
        public static double CalculateDiscount(params Product[] products)
        {
            var discount = products.Length * 0.01;
            return Math.Min(discount, 0.2);
        }
    }

}