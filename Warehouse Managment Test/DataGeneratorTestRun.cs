using Warehouse_Managemet_System.DataFaking;
using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managment_Test
{
    public class DataGeneratorTestRun
    {
        [Fact]
        public void TestRunGenerateProduct()
        {
            DataGenerator generator = new();
            Product product = generator.GenerateProduct(16);
            Console.WriteLine("product generated. Id: " + product.Id + "Name: " + product.Name + ", Price: " + product.Price);
        }
    }
}