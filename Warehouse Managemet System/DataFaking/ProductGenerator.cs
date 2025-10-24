using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

public class ProductGenerator : IRowGenerator
{
    private Faker<Product> productFaker;
    private int nextId = 0;

    public ProductGenerator()
    {
        productFaker = new Faker<Product>()
            .RuleFor(r => r.Id, f => "" + nextId++)
            .RuleFor(r => r.Name, f => f.Commerce.ProductName())
            .RuleFor(r => r.Price, f => f.Random.Double(0.0, 400.0));
    }

    public IRowModel Generate(int? seed = null)
    {
        if (seed is int s)
        {
            Randomizer.Seed = new Random(s);
        }
        Product product = productFaker.Generate();
        return product;
    }
}