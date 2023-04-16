using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Enums;
using Broker.Accounts.Domain.Rules;
using Broker.Core.Exceptions;
using Broker.Core.Rules;

namespace Broker.Accounts.Domain.Tests.Rules;

public class OrderPolicyTests
{
    private Account account;

    [SetUp]
    public void SetUp()
    {
        Issuers issuers = new();
        issuers.Add(new(new("GOOG"), new(8), new(50)));

        account = new(new(1), new(1040), issuers);
    }

    [Test(Description= "Set of rules for purchase order but after hours, should return CLOSED_MARKET")]
    public void TooLateOperation()
    {
        long date_2023_04_18_21_00_00 = 1681873200000;
        WriteOrder order = new(
            new(1),
            new(date_2023_04_18_21_00_00),
            new(OperationCode.BUY),
            new("NFTX"),
            new(1),
            new(80)
        );

        IBusinessRule<WriteOrder>[] rules = new IBusinessRule<WriteOrder>[]
        {
            new ClosedMarketRule(),
            new InsufficientBalanceRule(account)
        };

        OrdersPolicy policy = new();
        BusinessErrors errors = policy.Execute(order, rules);

        errors.ToArray().Should().NotBeEmpty().And.Contain("CLOSED_MARKET");
    }

    [Test(Description = "Set of rules for purchase order but after hours and insufficient balance, should return CLOSED_MARKET and INSUFFICIENT_BALANCE")]
    public void TooLateOperationAndInsufficientBalance() 
    {
        long date_2023_04_18_21_00_00 = 1681873200000;
        WriteOrder order = new(
            new(1),
            new(date_2023_04_18_21_00_00),
            new(OperationCode.BUY),
            new("NFTX"),
            new(100),
            new(80)
        );

        IBusinessRule<WriteOrder>[] rules = new IBusinessRule<WriteOrder>[]
        {
            new ClosedMarketRule(),
            new InsufficientBalanceRule(account)
        };

        OrdersPolicy policy = new();
        BusinessErrors errors = policy.Execute(order, rules);

        errors.ToArray().Should().NotBeEmpty()
            .And.HaveCount(2)
            .And.Contain("CLOSED_MARKET")
            .And.Contain("INSUFFICIENT_BALANCE");
    }

    [Test(Description = "Set of rules for sell order but after hours and insufficient stocks, should return CLOSED_MARKET and INSUFFICIENT_STOCKS")]
    public void TooLateOperationAndInsufficientStocks()
    {
        long date_2023_04_18_21_00_00 = 1681873200000;
        WriteOrder order = new(
            new(1),
            new(date_2023_04_18_21_00_00),
            new(OperationCode.SELL),
            new("NFTX"),
            new(100),
            new(80)
        );

        IBusinessRule<WriteOrder>[] rules = new IBusinessRule<WriteOrder>[]
        {
            new ClosedMarketRule(),
            new InsufficientStocksRule(account)
        };

        OrdersPolicy policy = new();
        BusinessErrors errors = policy.Execute(order, rules);

        errors.ToArray().Should().NotBeEmpty()
            .And.HaveCount(2)
            .And.Contain("CLOSED_MARKET")
            .And.Contain("INSUFFICIENT_STOCKS");
    }

    [Test(Description = "Set of rules for purchase order but a rule from the sales order context is included, should return InvalidRuleForContextException")]
    public void TryToExcuteOtherContextRule()
    {
        Action action = () => 
        {
            long date_2023_04_18_21_00_00 = 1681873200000;
            WriteOrder order = new(
                new(1),
                new(date_2023_04_18_21_00_00),
                new(OperationCode.SELL),
                new("NFTX"),
                new(1),
                new(80)
            );

            IBusinessRule<WriteOrder>[] rules = new IBusinessRule<WriteOrder>[]
            {
            new ClosedMarketRule(),
            new InsufficientStocksRule(account),
            new InsufficientBalanceRule(account) // Other context Rule
            };

            OrdersPolicy policy = new();
            BusinessErrors errors = policy.Execute(order, rules);
        };

        action.Should().Throw<InvalidRuleForContextException>();
    }
}
