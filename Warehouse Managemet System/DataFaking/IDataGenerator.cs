using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

/// <summary>
/// Class to generate rows of fake data (IRowModel instances)
/// </summary>
public interface IDataGenerator
{
    /// <summary>
    /// Generates a given number of rows of fake data
    /// </summary>
    /// <param name="rowGenerator">IRowGenerator instance, determines the IRowModel implementation of which to generate instances</param>
    /// <param name="amount">int, amount of rows to be generated</param>
    /// <param name="seed">optional int, set seed for rng before generating rows</param>
    /// <returns>List<IRowModel>, table of fake data (list of rows)</returns>
    public List<IRowModel> GenerateRows(IRowGenerator rowGenerator, int amount, int? seed = null);
    /// <summary>
    /// Generates a single row of fake data
    /// </summary>
    /// <param name="rowGenerator">IRowGenerator instance, determines the IRowModel implementation of which to generate instances</param>
    /// <param name="seed">optional int, set seed for rng before generating rows</param>
    /// <returns>IRowModel, row of fake data</returns>
    public IRowModel GenerateRow(IRowGenerator rowGenerator, int? seed = null);
}