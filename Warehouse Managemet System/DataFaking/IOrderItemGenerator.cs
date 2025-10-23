using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.DataFaking;

public interface IOrderItemGenerator
{
    public OrderItem GenerateOrderItem(List<Product> products, List<Order> orders, int? seed);
    public List<OrderItem> GenerateOrderitems(int amount, List<Product> products, List<Order> orders, int? seed);
}