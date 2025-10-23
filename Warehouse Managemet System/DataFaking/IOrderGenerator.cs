using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.DataFaking;

public interface IOrderGenerator
{
    public Order GenerateOrder(int? seed);
    public List<Order> GenerateOrders(int amount, int? seed);
}