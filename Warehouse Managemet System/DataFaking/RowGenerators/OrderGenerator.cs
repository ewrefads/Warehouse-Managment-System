using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class OrderGenerator : IRowGenerator
{
    private Faker<Order> orderFaker;
    private int nextId = 0;

    /// <summary>
    /// Constructor method
    /// </summary>
    /// <param name="transactionIds">List<string>, list of valid IDs for transactions</param>
    public OrderGenerator(List<string> transactionIds)
    {
        orderFaker = new Faker<Order>()
            .RuleFor(r => r.Id, f => "" + nextId++)
            .RuleFor(r => r.Customer, f => f.Name.FullName())
            .RuleFor(r => r.CreationTime, f => f.Date.Recent())
            .RuleFor(r => r.Status, f => f.PickRandom<OrderStatus>())
            .RuleFor(r => r.ActiveTransactionId, f => f.PickRandom(transactionIds));
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IRowModel Generate()
    {
        Order order = orderFaker.Generate();
        return order;
    }
}