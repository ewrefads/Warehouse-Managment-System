using Bogus;
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
            IRowModel product = generator.Generate(16);
            Console.WriteLine(product.ToString());
        }
        
        [Fact]
        public void TestRunGenerateWarehouse()
        {
            WarehouseGenerator generator = new();
            IRowModel wareHouse = generator.Generate(16);
            Console.WriteLine(wareHouse.ToString());
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