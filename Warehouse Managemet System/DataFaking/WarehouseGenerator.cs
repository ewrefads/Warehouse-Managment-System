using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

public class WarehouseGenerator : IRowGenerator
{
    private Faker<Warehouse> warehouseFaker;

    public WarehouseGenerator()
    {
        warehouseFaker = new Faker<Warehouse>()
            .RuleFor(r => r.Id, f => "")
            .RuleFor(r => r.Name, f => f.Address.City());
    }

    public IRowModel Generate(int? seed = null)
    {
        if (seed is int s)
        {
            Randomizer.Seed = new Random(s);
        }
        Warehouse warehouse = warehouseFaker.Generate();
        return warehouse;
    }
}