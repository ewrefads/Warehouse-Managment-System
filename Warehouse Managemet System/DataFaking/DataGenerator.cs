using Bogus;
using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.DataFaking;

public class DataGenerator : IDataGenerator
{
    public Product GenerateProduct(int? seed)
    {
        if (seed is int s)
        {
            Randomizer.Seed = new Random(s);
        }
        Faker<Product> productFaker = new Faker<Product>()
            .RuleFor(r => r.Id, f => "")
            .RuleFor(r => r.Name, f => f.Commerce.ProductName())
            .RuleFor(r => r.Price, f => f.Random.Double(0.0, 400.0));
        Product product = productFaker.Generate();
        return product;
    }

    public Warehouse GenerateWarehouse(int? seed)
    {
        return null;
    }

    public InventoryItem GenerateInventoryItem(int? seed)
    {
        return null;
    }

    public Order GenerateOrder(int? seed)
    {
        return null;
    }

    public OrderItem GenerateOrderItem(int? seed)
    {
        return null;
    }

    public Transaction GenerateTransaction(int? seed)
    {
        return null;
    }

    public List<Product> GenerateRows<RowModel>(int amount, int? seed) where RowModel : IRowModel
    {
        return null;
    }
}