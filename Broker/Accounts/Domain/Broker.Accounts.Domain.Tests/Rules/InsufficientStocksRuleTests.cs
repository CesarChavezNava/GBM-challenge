using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Enums;
using Broker.Accounts.Domain.Rules;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Core.Rules;

namespace Broker.Accounts.Domain.Tests.Rules;

[TestFixture]
public class InsufficientStocksRuleTests 
{
    private InsufficientStocksRule rule;

    [SetUp]
    public void SetUp()
    {
        Issuers issuers = new();
        issuers.Add(
            new Issuer(
                new IssuerName("AAPL"),
                new TotalShares(2),
                new SharePrice(50)
            )
        );
        issuers.Add(
            new Issuer(
                new IssuerName("NFTX"),
                new TotalShares(10),
                new SharePrice(80)
            )
        );

        Balance currentBalance = new(new Cash(1000), issuers);
        rule = new(currentBalance);
    }

    [Test(Description = "Too low stocks, should return INSUFFICIENT_STOCKS")]
    public void TooLowStocks()
    {
        long date_2023_04_18_12_00_00 = 1681840800000;

        WriteOrder order = new(
            new UserId(1),
            new Timestamp(date_2023_04_18_12_00_00),
            new Operation(OperationCode.SELL),
            new IssuerName("AAPL"),
            new TotalShares(5),
            new SharePrice(50)
        );

        BusinessErrors errors = new();
        rule.Execute(order, errors);

        errors.ToArray().Should().NotBeEmpty().And.Contain("INSUFFICIENT_STOCKS");
    }

    [Test(Description = "No stocks, should return INSUFFICIENT_STOCKS")]
    public void NoStocks()
    {
        long date_2023_04_18_12_00_00 = 1681840800000;

        WriteOrder order = new(
            new UserId(1),
            new Timestamp(date_2023_04_18_12_00_00),
            new Operation(OperationCode.SELL),
            new IssuerName("GOOG"),
            new TotalShares(1),
            new SharePrice(70)
        );

        BusinessErrors errors = new();
        rule.Execute(order, errors);

        errors.ToArray().Should().NotBeEmpty().And.Contain("INSUFFICIENT_STOCKS");
    }

    [Test(Description = "Exact Stocks, should return empty business errors")]
    public void ExtactStocks()
    {
        long date_2023_04_18_12_00_00 = 1681840800000;

        WriteOrder order = new(
            new UserId(1),
            new Timestamp(date_2023_04_18_12_00_00),
            new Operation(OperationCode.SELL),
            new IssuerName("AAPL"),
            new TotalShares(2),
            new SharePrice(60)
        );

        BusinessErrors errors = new();
        rule.Execute(order, errors);

        errors.ToArray().Should().BeEmpty();
    }

    [Test(Description = "Enough stocks, should return empty business errors")]
    public void EnoughStocks()
    {
        long date_2023_04_18_12_00_00 = 1681840800000;

        WriteOrder order = new(
            new UserId(1),
            new Timestamp(date_2023_04_18_12_00_00),
            new Operation(OperationCode.SELL),
            new IssuerName("NFTX"),
            new TotalShares(3),
            new SharePrice(100)
        );

        BusinessErrors errors = new();
        rule.Execute(order, errors);

        errors.ToArray().Should().BeEmpty();
    }
}
