using Bogus;
using Warehouse_Managemet_System.DataFaking;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managment_Test
{
    public class DataGeneratorTestRun
    {
        [Fact]
        public void TestRunGenerateProduct()
        {
            ProductGenerator generator = new();
            IRowModel product = generator.Generate();
            Console.WriteLine(product.ToString());
        }
        
        [Fact]
        public void TestRunGenerateWarehouse()
        {
            WarehouseGenerator generator = new();
            IRowModel wareHouse = generator.Generate();
            Console.WriteLine(wareHouse.ToString());
        }
        
        [Fact]
        public void TestRunGenerateProducts()
        {
            ProductGenerator rowGenerator = new();
            DataGenerator generator = new();
            List<IRowModel> products = generator.GenerateRows(rowGenerator, 3, 16);
            foreach (IRowModel product in products)
            {
                Console.WriteLine(product.ToString());
            }
        }
        
        [Fact]
        public void TestRunGenerateInventoryItem()
        {
            List<string> productIds = new() {"p1", "p2", "p3"};
            List<string> warehouseIds = new() {"w1", "w2", "w3"};
            InventoryItemGenerator generator = new(productIds, warehouseIds);
            IRowModel inventoryItem = generator.Generate();
            Console.WriteLine(inventoryItem.ToString());
        }
        
        [Fact]
        public void TestRunGenerateTransaction()
        {
            List<string> productIds = new() {"p1", "p2", "p3"};
            List<string> warehouseIds = new() {"w1", "w2", "w3"};
            TransactionGenerator generator = new(productIds, warehouseIds);
            IRowModel transaction = generator.Generate();
            Console.WriteLine(transaction.ToString());
        }
    }
}