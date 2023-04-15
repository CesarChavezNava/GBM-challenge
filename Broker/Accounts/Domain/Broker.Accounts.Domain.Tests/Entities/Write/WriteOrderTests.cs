using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Enums;
using Broker.Accounts.Domain.Exceptions;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Tests.Entities.Write;

[TestFixture]
public class WriteOrderTests
{
    private WriteOrder expected;

    [SetUp]
    public void SetUp()
    {
        expected = new(
            new UserId(1),
            new Timestamp(1681840800000),
            new Operation(OperationCode.BUY),
            new IssuerName("MSFT"),
            new TotalShares(1),
            new SharePrice(5174.99m)
        );
    }

    [Test(Description = "Invalid WriteOrder because the share price is 0, should return TooLowSharePriceException")]
    public void OrderWithSharePrice0()
    {
        Action action = () => new WriteOrder(
            new UserId(1),
            new Timestamp(1681840800000),
            new Operation(OperationCode.BUY),
            new IssuerName("MSFT"),
            new TotalShares(1),
            new SharePrice(0)
        );

        action.Should().Throw<TooLowSharePriceException>();
    }

    [Test(Description = "Valid WriteOrder, should return expected")]
    public void ValidWriteAccount()
    {
        WriteOrder order = new(
            new UserId(1),
            new Timestamp(1681840800000),
            new Operation(OperationCode.BUY),
            new IssuerName("MSFT"),
            new TotalShares(1),
            new SharePrice(5174.99m)
        );

        order.Should().NotBeNull();
        order.UserId.Value.Should().Be(expected.UserId.Value);
        order.Timestamp.Value.Should().Be(expected.Timestamp.Value);
        order.Operation.Value.Should().Be(expected.Operation.Value);
        order.IssuerName.Value.Should().Be(expected.IssuerName.Value);
        order.TotalShares.Value.Should().Be(expected.TotalShares.Value);
        order.SharePrice.Value.Should().Be(expected.SharePrice.Value);
    }
}
