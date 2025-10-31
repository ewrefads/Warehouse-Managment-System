using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

/// <summary>
/// Used for generating fake data rows (instances of IRowModel)
/// </summary>
public interface IRowGenerator
{
    /// <summary>
    /// Generates a data row
    /// </summary>
    /// <returns>IRowModel instance, fake row</returns>
    public IRowModel Generate();
}