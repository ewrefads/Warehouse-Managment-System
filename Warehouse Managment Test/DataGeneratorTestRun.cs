using Warehouse_Managemet_System.DataFaking;
using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managment_Test
{
    public class DataGeneratorTestRun
    {
        [Fact]
        public void TestRunGenerateProduct()
        {
            ProductGenerator generator = new();
            Product product = generator.GenerateProduct(16);
            Console.WriteLine("product generated. Id: " + product.Id + "Name: " + product.Name + ", Price: " + product.Price);
        }
        
        [Fact]
        public void TestRunGenerateWarehouse()
        {
            WarehouseGenerator generator = new();
            Warehouse warehouse = generator.GenerateWarehouse(16);
            Console.WriteLine("warehouse generated. Id: " + warehouse.Id + "Name: " + warehouse.Name);
        }
        
        [Fact]
        public void TestRunGenerateInventoryItem()
        {
            //InventoryItemGenerator generator = new();
            //InventoryItem item = generator.GenerateInventoryItem(16);
            //Console.WriteLine("inventory item generated. Id: " + item.Id + "Warehouse Id: " + item.WarehouseId + "Product Id: " + item.ProductId + "Amount: " + item.Amount);
        }
    }
}