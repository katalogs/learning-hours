using System.Collections.Immutable;

namespace Mutation.Customer
{
    public sealed record Store(ImmutableDictionary<ProductType, int> Inventory) : IStore
    {
        public Store AddInventory(ProductType product, int quantity)
            => UpdateStore(product, GetInventoryFor(product) + quantity);

        public Store RemoveInventory(ProductType product, int quantity)
            => UpdateStore(product, GetInventoryFor(product) - quantity);

        public bool HasEnoughInventory(ProductType product, int quantity)
            => GetInventoryFor(product) >= quantity;

        public int GetInventoryFor(ProductType product)
            => Inventory.ContainsKey(product)
                ? Inventory[product]
                : 0;

        public Store UpdateStore(ProductType product, int newQuantity)
            => new(Inventory.SetItem(product, newQuantity));
    }
}