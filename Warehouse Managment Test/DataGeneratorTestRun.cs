using Bogus;
using Warehouse_Managemet_System.DataFaking;
using Warehouse_Managemet_System.RowModels;
using Warehouse_Management_Test.Mocks.RowModels;

namespace Warehouse_Management_Test
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
        
        [Fact]
        public void TestRunGenerateOrder()
        {
            List<string> transactionIds = new() {"t1", "t2", "t3"};
            OrderGenerator generator = new(transactionIds);
            IRowModel order = generator.Generate();
            Console.WriteLine(order.ToString());
        }
        
        [Fact]
        public void TestRunGenerateOrderItem()
        {
            List<string> productIds = new() {"p1", "p2", "p3"};
            List<string> orderIds = new() {"o1", "o2", "o3"};
            OrderItemGenerator generator = new(productIds, orderIds);
            IRowModel orderItem = generator.Generate();
            Console.WriteLine(orderItem.ToString());
        }
        
        [Fact]
        public void TestRunGenerateDataFile()
        {
            QueryTestRowModel mock1 = new();
            QueryTestRowModel mock2 = new();
            List<IRowModel> mocks = new List<IRowModel> {mock1, mock2};
            DataFileGenerator fileGenerator = new();
            string path = "C:\\Users\\Asger Harpøth Møller\\Documents\\Specialisterne opgaver\\uge-6-7\\Warehouse-Managment-System\\Warehouse-Managment-System\\Warehouse Managemet System\\DataFaking\\testfile.csv";
            fileGenerator.GenerateDataFile(path, mocks);
        }
    }
}