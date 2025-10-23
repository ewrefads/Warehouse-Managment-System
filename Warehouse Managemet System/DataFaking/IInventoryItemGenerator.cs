using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.DataFaking;

public interface IInventoryItemGenerator
{
    public InventoryItem GenerateInventoryItem(List<Product> products, List<Warehouse> warehouses, int? seed);
    public List<InventoryItem> GenerateInventoryitems(int amount, List<Product> products, List<Warehouse> warehouses, int? seed);
}