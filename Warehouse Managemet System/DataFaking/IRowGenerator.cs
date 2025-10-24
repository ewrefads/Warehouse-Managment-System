using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

public interface IRowGenerator
{
    public IRowModel Generate(int? seed);
}