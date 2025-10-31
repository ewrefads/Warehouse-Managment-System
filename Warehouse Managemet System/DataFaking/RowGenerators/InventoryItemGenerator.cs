using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class InventoryItemGenerator : IRowGenerator
{
    private Faker<InventoryItem> inventoryItemFaker;
    private int nextId = 0;

    /// <summary>
    /// Constructor method
    /// </summary>
    /// <param name="productIds">List<string>, list of valid IDs for products</param>
    /// <param name="warehouseIds">List<string>, list of valid IDs for warehouses</param>
    public InventoryItemGenerator(List<string> productIds, List<string> warehouseIds)
    {
        inventoryItemFaker = new Faker<InventoryItem>()
            .RuleFor(r => r.Id, f => "" + nextId++)
            .RuleFor(r => r.WarehouseId, f => f.PickRandom(warehouseIds))
            .RuleFor(r => r.ProductId, f => f.PickRandom(productIds))
            .RuleFor(r => r.Amount, f => f.Random.Int(1, 4000));
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IRowModel Generate()
    {
        InventoryItem inventoryItem = inventoryItemFaker.Generate();
        return inventoryItem;
    }
}