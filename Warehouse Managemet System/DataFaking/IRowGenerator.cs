using Bogus;
using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.DataFaking;

public interface IRowGenerator
{
    public IRowModel Generate(int? seed);
}