using Bogus;
using Warehouse_Managemet_System.Table_Models;

namespace Warehouse_Managemet_System.DataFaking;

public class ProductGenerator : IProductGenerator
{
    public Product GenerateProduct(int? seed = null)
    {
        if (seed is int s)
        {
            Randomizer.Seed = new Random(s);
        }
        Faker<Product> productFaker = new Faker<Product>()
            .RuleFor(r => r.Id, f => "")
            .RuleFor(r => r.Name, f => f.Commerce.ProductName())
            .RuleFor(r => r.Price, f => f.Random.Double(0.0, 400.0));
        Product product = productFaker.Generate();
        return product;
    }

    public List<Product> GenerateProducts(int amount, int? seed = null)
    {
        if (seed is int s)
        {
            Randomizer.Seed = new Random(s);
        }
        List<Product> products = new();
        for (int i = 0; i < amount; i++)
        {
            Product product = GenerateProduct();
            products.Add(product);
        }
        return products;
    }
}