using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Enums;
using Broker.Accounts.Domain.Rules;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Core.Rules;

namespace Broker.Accounts.Domain.Tests.Rules;

[TestFixture]
public class InsufficientBalanceRuleTests
{
    private InsufficientBalanceRule rule;

    [SetUp]
    public void SetUp()
    {
        Account account = new(new(1), new(1040), new Issuers());
        rule = new(account);
    }

    [Test(Description = "Too Low Balance, should return INSUFFICIENT_BALANCE")]
    public void TooLowBalance()
    {
        long date_2023_04_18_12_00_00 = 1681840800000;

        WriteOrder order = new(
            new UserId(1),
            new Timestamp(date_2023_04_18_12_00_00),
            new Operation(OperationCode.BUY),
            new IssuerName("NFTX"),
            new TotalShares(50),
            new SharePrice(80)
        );

        BusinessErrors errors = new();
        rule.Execute(order, errors);

        errors.ToArray().Should().NotBeEmpty().And.Contain("INSUFFICIENT_BALANCE");
    }

    [Test(Description = "Exact Balance, should return empty business errors")]
    public void ExactBalance()
    {
        long date_2023_04_18_12_00_00 = 1681840800000;

        WriteOrder order = new(
            new UserId(1),
            new Timestamp(date_2023_04_18_12_00_00),
            new Operation(OperationCode.BUY),
            new IssuerName("NFTX"),
            new TotalShares(13),
            new SharePrice(80)
        );

        BusinessErrors errors = new();
        rule.Execute(order, errors);

        errors.ToArray().Should().BeEmpty();
    }

    [Test(Description = "Enough Balance, should return empty business errors")]
    public void EnoughBalance()
    {
        long date_2023_04_18_12_00_00 = 1681840800000;

        WriteOrder order = new(
            new UserId(1),
            new Timestamp(date_2023_04_18_12_00_00),
            new Operation(OperationCode.BUY),
            new IssuerName("NFTX"),
            new TotalShares(2),
            new SharePrice(80)
        );

        BusinessErrors errors = new();
        rule.Execute(order, errors);

        errors.ToArray().Should().BeEmpty();
    }
}
