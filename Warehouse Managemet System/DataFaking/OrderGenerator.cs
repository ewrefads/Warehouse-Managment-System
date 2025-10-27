using Bogus;
using Warehouse_Managemet_System.RowModels;

namespace Warehouse_Managemet_System.DataFaking;

public class OrderGenerator : IRowGenerator
{
    private Faker<Order> orderFaker;
    private int nextId = 0;

    public OrderGenerator(List<string> transactionIds)
    {
        orderFaker = new Faker<Order>()
            .RuleFor(r => r.Id, f => "" + nextId++)
            .RuleFor(r => r.Customer, f => f.Name.FullName())
            .RuleFor(r => r.CreationTime, f => f.Date.Recent())
            .RuleFor(r => r.Status, f => f.PickRandom<OrderStatus>())
            .RuleFor(r => r.ActiveTransactionId, f => f.PickRandom(transactionIds));
    }

    public IRowModel Generate()
    {
        Order order = orderFaker.Generate();
        return order;
    }
}