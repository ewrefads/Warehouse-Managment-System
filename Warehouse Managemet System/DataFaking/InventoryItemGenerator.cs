using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

public class InventoryItemGenerator : IRowGenerator
{
    private Faker<InventoryItem> inventoryItemFaker;
    private int nextId = 0;

    public InventoryItemGenerator(List<string> productIds, List<string> warehouseIds)
    {
        inventoryItemFaker = new Faker<InventoryItem>()
            .RuleFor(r => r.Id, f => "" + nextId++)
            .RuleFor(r => r.WarehouseId, f => f.PickRandom(warehouseIds))
            .RuleFor(r => r.ProductId, f => f.PickRandom(productIds))
            .RuleFor(r => r.Amount, f => f.Random.Int(1, 4000));
    }

    public IRowModel Generate(int? seed = null)
    {
        if (seed is int s)
        {
            Randomizer.Seed = new Random(s);
        }
        InventoryItem inventoryItem = inventoryItemFaker.Generate();
        return inventoryItem;
    }
}