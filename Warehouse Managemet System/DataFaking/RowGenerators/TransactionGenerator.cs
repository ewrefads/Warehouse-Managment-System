using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class TransactionGenerator : IRowGenerator
{
    private Faker<Transaction> transactionFaker;
    private int nextId = 0;

    /// <summary>
    /// Constructor method
    /// </summary>
    /// <param name="productIds">List<string>, list of valid IDs for products</param>
    /// <param name="warehouseIds">List<string>, list of valid IDs for warehouses</param>
    public TransactionGenerator(List<string> productIds, List<string> warehouseIds)
    {
        transactionFaker = new Faker<Transaction>()
            .RuleFor(r => r.Id, f => "" + nextId++)
            .RuleFor(r => r.ProductId, f => f.PickRandom(productIds))
            .RuleFor(r => r.Type, f => f.PickRandom<TransactionType>())
            .RuleFor(r => r.Amount, f => f.Random.Int(1, 200))
            .RuleFor(r => r.Status, f => f.PickRandom<TransactionStatus>())
            .RuleFor(r => r.FromWarehouseId, (f, r) => (r.Type == TransactionType.Return)
                ? null : f.PickRandom(warehouseIds))
            .RuleFor(r => r.ToWarehouseId, (f, r) => (r.Type == TransactionType.Sale)
                ? null : f.PickRandom(warehouseIds));
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IRowModel Generate()
    {
        Transaction transaction = transactionFaker.Generate();
        return transaction;
    }
}