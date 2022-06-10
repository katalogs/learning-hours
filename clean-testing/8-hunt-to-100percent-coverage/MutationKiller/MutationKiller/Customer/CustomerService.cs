using LanguageExt;

namespace Mutation.Customer
{
    public static class CustomerService
    {
        public static Try<Store> Purchase(
            IStore store,
            ProductType product,
            int quantity)
            => () => !store.HasEnoughInventory(product, quantity)
                ? throw new ArgumentException("Not enough inventory")
                : store.RemoveInventory(product, quantity);
    }
}