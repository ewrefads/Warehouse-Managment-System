using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

public class OrderItemGenerator : IRowGenerator
{
    private Faker<OrderItem> orderItemFaker;
    private int nextId = 0;

    public OrderItemGenerator(List<string> productIds, List<string> orderIds)
    {
        orderItemFaker = new Faker<OrderItem>()
            .RuleFor(r => r.Id, f => "" + nextId++)
            .RuleFor(r => r.OrderId, f => f.PickRandom(orderIds))
            .RuleFor(r => r.ProductId, f => f.PickRandom(productIds))
            .RuleFor(r => r.Amount, f => f.Random.Int(1, 100));
    }

    public IRowModel Generate()
    {
        OrderItem orderItem = orderItemFaker.Generate();
        return orderItem;
    }
}