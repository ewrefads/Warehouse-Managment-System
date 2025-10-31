using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

public interface IDataGenerator
{
    public List<IRowModel> GenerateRows(IRowGenerator rowGenerator, int amount, int? seed = null);
    public IRowModel GenerateRow(IRowGenerator rowGenerator, int? seed = null);
}