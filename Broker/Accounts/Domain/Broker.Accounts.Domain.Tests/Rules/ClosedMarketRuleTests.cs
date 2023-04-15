using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Enums;
using Broker.Accounts.Domain.Rules;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Core.Rules;

namespace Broker.Accounts.Domain.Tests.Rules;

[TestFixture]
public class ClosedMarketRuleTests
{
    private ClosedMarketRule rule;

    [SetUp]
    public void SetUp()
    {
        rule = new();
    }

    [Test(Description = "Order before opening, should return CLOSED_MARKET")]
    public void TooEarly()
    {
        long date_2023_04_18_05_00_00 = 1681815600000;

        WriteOrder order = new(
            new UserId(1),
            new Timestamp(date_2023_04_18_05_00_00),
            new Operation(OperationCode.BUY),
            new IssuerName("NFTX"),
            new TotalShares(10),
            new SharePrice(80)
        );

        BusinessErrors errors = new();
        rule.Execute(order, errors);

        errors.ToArray().Should().NotBeEmpty().And.Contain("CLOSED_MARKET");
    }

    [Test(Description = "Order after closing, should return CLOSED_MARKET")]
    public void TooLate()
    {
        long date_2023_04_18_21_00_00 = 1681873200000;

        WriteOrder order = new(
            new UserId(1),
            new Timestamp(date_2023_04_18_21_00_00),
            new Operation(OperationCode.BUY),
            new IssuerName("NFTX"),
            new TotalShares(10),
            new SharePrice(80)
        );

        BusinessErrors errors = new();
        rule.Execute(order, errors);

        errors.ToArray().Should().NotBeEmpty().And.Contain("CLOSED_MARKET");
    }

    [Test(Description = "Order in time, should return empty business errors")]
    public void InTime()
    {
        long date_2023_04_18_12_00_00 = 1681840800000;

        WriteOrder order = new(
            new UserId(1),
            new Timestamp(date_2023_04_18_12_00_00),
            new Operation(OperationCode.SELL),
            new IssuerName("NFTX"),
            new TotalShares(10),
            new SharePrice(80)
        );

        BusinessErrors errors = new();
        rule.Execute(order, errors);

        errors.ToArray().Should().BeEmpty();
    }
}
