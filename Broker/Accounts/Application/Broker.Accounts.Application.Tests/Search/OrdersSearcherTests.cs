using Broker.Accounts.Application.Search;
using Broker.Accounts.Domain.Entities.Criteria;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Enums;
using Broker.Accounts.Domain.Repositories;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Core.Entities;
using Broker.Core.Enums;
using Moq;

namespace Broker.Accounts.Application.Tests.Search;

[TestFixture]
public class OrdersSearcherTests
{
    [Test]
    public async Task Search()
    {
        Orders ordersExpected = new Orders(
            new List<Order>()
            {
                new Order(new(1681840800000), new(OperationCode.BUY), new("AAPL"), new TotalShares(10), new(2599.89m))
            }
        );

        Criteria<OrderFilters> criteria = new(
            new(new(1), "AAPL", "BUY"),
            new(SearchOrderCode.DESC)
        );

        Mock<IOrderRepository> orderRepository = new();
        OrdersSearcher ordersSearchers = new(orderRepository.Object);

        orderRepository.Setup(a => a.Search(It.IsAny<Criteria<OrderFilters>>())).Returns(() => Task.FromResult(ordersExpected));
        Orders orders = await ordersSearchers.Search(criteria);

        orders.ToArray().Should().HaveCount(ordersExpected.Count);

        orderRepository.Setup(a => a.Search(It.IsAny<Criteria<OrderFilters>>()));
    }
}
