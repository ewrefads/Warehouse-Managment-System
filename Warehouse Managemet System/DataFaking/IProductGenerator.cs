using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.DataFaking;

public interface IProductGenerator
{
    public Product GenerateProduct(int? seed);
    public List<Product> GenerateProducts(int amount, int? seed);
}