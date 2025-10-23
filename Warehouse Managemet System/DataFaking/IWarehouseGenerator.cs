using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.DataFaking;

public interface IWarehouseGenerator
{
    public Warehouse GenerateWarehouse(int? seed);
    public List<Warehouse> GenerateWarehouses(int amount, int? seed);
}