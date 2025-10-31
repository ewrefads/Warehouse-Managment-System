using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class WarehouseGenerator : IRowGenerator
{
    private Faker<Warehouse> warehouseFaker;
    private int nextId = 0;

    /// <summary>
    /// Constructor method
    /// </summary>
    public WarehouseGenerator()
    {
        warehouseFaker = new Faker<Warehouse>()
            .RuleFor(r => r.Id, f => "" + nextId++)
            .RuleFor(r => r.Name, f => f.Address.City());
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IRowModel Generate()
    {
        Warehouse warehouse = warehouseFaker.Generate();
        return warehouse;
    }
}