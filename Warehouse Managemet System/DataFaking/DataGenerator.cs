using Bogus;
using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.DataFaking;

public class DataGenerator
{
    public List<IRowModel> Generate(IRowGenerator rowGenerator, int amount, int? seed)
    {
        if (seed is int s)
        {
            Randomizer.Seed = new Random(s);
        }
        List<IRowModel> rows = new();
        for (int i = 0; i < amount; i++)
        {
            IRowModel row = rowGenerator.Generate(null);
            rows.Add(row);
        }
        return rows;
    }
}