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
        public void TestRunGenerateProducts()
        {
            ProductGenerator rowGenerator = new();
            DataGenerator generator = new();
            List<IRowModel> products = generator.Generate(rowGenerator, 3, 16);
            foreach (IRowModel product in products)
            {
                Console.WriteLine(product.ToString());
            }
        }
    }
}