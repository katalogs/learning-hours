namespace Mutation.Customer
{
    public interface IStore
    {
        Store AddInventory(ProductType product, int quantity);
        Store RemoveInventory(ProductType product, int quantity);
        bool HasEnoughInventory(ProductType product, int quantity);
        int GetInventoryFor(ProductType product);
        Store UpdateStore(ProductType product, int newQuantity);
    }
}