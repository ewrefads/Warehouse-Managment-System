using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

public class DataGenerator
{
    public List<IRowModel> GenerateRows(IRowGenerator rowGenerator, int amount, int? seed = null)
    {
        SetSeed(seed);
        List<IRowModel> rows = new();
        for (int i = 0; i < amount; i++)
        {
            IRowModel row = GenerateRow(rowGenerator, null);
            rows.Add(row);
        }
        return rows;
    }

    public IRowModel GenerateRow(IRowGenerator rowGenerator, int? seed = null)
    {
        SetSeed(seed);
        IRowModel row = rowGenerator.Generate();
        return row;
    }

    public void SetSeed(int? seed)
    {
        if (seed is int s)
        {
            Randomizer.Seed = new Random(s);
        }
    }
}