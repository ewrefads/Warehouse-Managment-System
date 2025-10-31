using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
public class DataGenerator : IDataGenerator
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IRowModel GenerateRow(IRowGenerator rowGenerator, int? seed = null)
    {
        SetSeed(seed);
        IRowModel row = rowGenerator.Generate();
        return row;
    }

    private void SetSeed(int? seed)
    {
        if (seed is int s)
        {
            Randomizer.Seed = new Random(s);
        }
    }
}