using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class ProductGenerator : IRowGenerator
{
    private Faker<Product> productFaker;
    private int nextId = 0;

    /// <summary>
    /// Constructor method
    /// </summary>
    public ProductGenerator()
    {
        productFaker = new Faker<Product>()
            .RuleFor(r => r.Id, f => "" + nextId++)
            .RuleFor(r => r.ProductName, f => f.Commerce.ProductName())
            .RuleFor(r => r.Price, f => f.Random.Double(0.0, 400.0));
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IRowModel Generate()
    {
        Product product = productFaker.Generate();
        return product;
    }
}