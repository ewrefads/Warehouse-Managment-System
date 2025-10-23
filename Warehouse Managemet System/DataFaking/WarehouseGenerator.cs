using Bogus;
using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.DataFaking;

public class WarehouseGenerator : IWarehouseGenerator
{

    public Warehouse GenerateWarehouse(int? seed = null)
    {
        if (seed is int s)
        {
            Randomizer.Seed = new Random(s);
        }
        Faker<Warehouse> warehouseFaker = new Faker<Warehouse>()
            .RuleFor(r => r.Id, f => "")
            .RuleFor(r => r.Name, f => f.Address.City());
        Warehouse warehouse = warehouseFaker.Generate();
        return warehouse;
    }

    public List<Warehouse> GenerateWarehouses(int amount, int? seed = null)
    {
        if (seed is int s)
        {
            Randomizer.Seed = new Random(s);
        }
        List<Product> products = new();
        for (int i = 0; i < amount; i++)
        {
            Product product = GenerateProduct();
            products.Add(product);
        }
        return products;
    }
}