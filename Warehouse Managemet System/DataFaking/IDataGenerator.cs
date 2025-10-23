namespace Warehouse_Managemet_System.DataFaking;

public interface IDataGenerator
{
    public Product GenerateProduct(int? seed);
    public Warehouse GenerateWarehouse(int? seed);
    public InventoryItem GenerateInventoryItem(int? seed);
    public Order GenerateOrder(int? seed);
    public OrderItem GenerateOrderItem(int? seed);
    public Transaction GenerateTransaction(int? seed);

    public List<Product> GenerateRows<RowModel>(int amount, int? seed) where RowModel : IRowModel;
}