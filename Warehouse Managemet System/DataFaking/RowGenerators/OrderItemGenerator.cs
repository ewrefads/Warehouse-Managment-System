using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class OrderItemGenerator : IRowGenerator
{
    private Faker<OrderItem> orderItemFaker;
    private int nextId = 0;

    /// <summary>
    /// Constructor method
    /// </summary>
    /// <param name="productIds">List<string>, list of valid IDs for products</param>
    /// <param name="orderIds">List<string>, list of valid IDs for orders</param>
    public OrderItemGenerator(List<string> productIds, List<string> orderIds)
    {
        orderItemFaker = new Faker<OrderItem>()
            .RuleFor(r => r.Id, f => "" + nextId++)
            .RuleFor(r => r.OrderId, f => f.PickRandom(orderIds))
            .RuleFor(r => r.ProductId, f => f.PickRandom(productIds))
            .RuleFor(r => r.Amount, f => f.Random.Int(1, 100));
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IRowModel Generate()
    {
        OrderItem orderItem = orderItemFaker.Generate();
        return orderItem;
    }
}