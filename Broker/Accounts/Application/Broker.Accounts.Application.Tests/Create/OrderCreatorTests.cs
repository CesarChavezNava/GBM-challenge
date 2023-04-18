using Broker.Accounts.Application.Create;
using Broker.Accounts.Domain.Entities.Criteria;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Enums;
using Broker.Accounts.Domain.Repositories;
using Broker.Accounts.Domain.Rules;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Core.Entities;
using Moq;

namespace Broker.Accounts.Application.Tests.Create;

[TestFixture]
public class OrderCreatorTests
{
    [Test(Description = "Create buy order")]
    public async Task Create()
    {
        WriteOrder order = new(new(1), new(1681840810000), new(OperationCode.BUY), new("AAPL"), new(1), new(2659.10m));
        Issuers issuers = new Issuers(
            new List<Issuer>()
            {
                new Issuer(new("AAPL"), new(10), new(2599.89m))
            }
        );
        Criteria<OrderFilters> criteria = new(new(new(1), order.IssuerName.Value, order.Operation.Value.ToString(), 5, order.Timestamp.Value));
        Orders orders = new Orders(
            new List<Order>()
            {
                new Order(new(1681840800000), new(OperationCode.BUY), new("AAPL"), new TotalShares(10), new(2599.89m))
            }
        );

        Mock<IAccountRepository> accountRepository = new();
        Mock<IOrderRepository> orderRepository = new();
        accountRepository.Setup(a => a.Find(It.IsAny<UserId>())).Returns(() => Task.FromResult(new Account(new(1), new(10000), issuers)));
        orderRepository.Setup(a => a.Search(It.IsAny<Criteria<OrderFilters>>())).Returns(() => Task.FromResult(orders));

        OrderCreator orderCreator = new(accountRepository.Object, orderRepository.Object, new OrdersPolicy());
        Account account = await orderCreator.Create(order);

        account.Cash.Value.Should().Be(10000 - 2659.10m);
        account.Issuers.Should().HaveCount(1);
        account.Issuers.First().TotalShares.Value.Should().Be(11);

        accountRepository.Verify(a => a.Find(It.IsAny<UserId>()));
        orderRepository.Verify(a => a.Search(It.IsAny<Criteria<OrderFilters>>()));
    }

    [Test(Description = "Create buy order but It's too late, should return CLOSED_MARKET")]
    public async Task CreateTooLate()
    {
        WriteOrder order = new(new(1), new(1681873200000), new(OperationCode.BUY), new("AAPL"), new(1), new(2659.10m));
        Issuers issuers = new Issuers(
            new List<Issuer>()
            {
                new Issuer(new("AAPL"), new(10), new(2599.89m))
            }
        );
        Criteria<OrderFilters> criteria = new(new(new(1), order.IssuerName.Value, order.Operation.Value.ToString(), 5, order.Timestamp.Value));
        Orders orders = new Orders(
            new List<Order>()
            {
                new Order(new(1681840800000), new(OperationCode.BUY), new("AAPL"), new TotalShares(10), new(2599.89m))
            }
        );

        Mock<IAccountRepository> accountRepository = new();
        Mock<IOrderRepository> orderRepository = new();
        accountRepository.Setup(a => a.Find(It.IsAny<UserId>())).Returns(() => Task.FromResult(new Account(new(1), new(10000), issuers)));
        orderRepository.Setup(a => a.Search(It.IsAny<Criteria<OrderFilters>>())).Returns(() => Task.FromResult(orders));

        OrderCreator orderCreator = new(accountRepository.Object, orderRepository.Object, new OrdersPolicy());
        Account account = await orderCreator.Create(order);

        account.BusinessErrors.ToArray().Should().Contain("CLOSED_MARKET");

        accountRepository.Verify(a => a.Find(It.IsAny<UserId>()));
        orderRepository.Verify(a => a.Search(It.IsAny<Criteria<OrderFilters>>()));
    }
}
