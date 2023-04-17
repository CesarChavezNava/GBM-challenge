using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Enums;
using Broker.Accounts.Domain.Rules;
using Broker.Core.Rules;

namespace Broker.Accounts.Domain.Tests.Rules;

[TestFixture]
public class DuplicateOperationRuleTests
{
    private Account account;
    private DuplicateOperationRule rule;

    [SetUp]
    public void SetUp()
    {
        long date_2023_04_18_12_04_00 = 1681841040000;

        Orders orders = new();
        orders.Add(new(new(date_2023_04_18_12_04_00), new(OperationCode.BUY), new("AAPL"), new(10), new(4650.89m)));

        account = new(new(1), new(999999.99m));
        account.AddOrders(orders);

        rule = new(account);
    }

    [Test(Description = "Duplicate operation in less than 5 minutes, shoul return DUPLICATE_OPERATION")]
    public void DuplicateOperationInLessThan5Min()
    {
        long date_2023_04_18_12_00_00 = 1681840800000;
        WriteOrder order = new(
            new(1), 
            new(date_2023_04_18_12_00_00), 
            new(OperationCode.BUY), 
            new("AAPL"), 
            new(10), 
            new(4650.89m)
        );
        
        BusinessErrors errors = new();
        rule.Execute(order, errors);

        errors.ToArray().Should().NotBeEmpty().And.Contain("DUPLICATE_OPERATION");
    }

    [Test(Description = "Duplicate operation but after 5 minutes, shoul return empty business errors")]
    public void DuplicateOperationAfter5Min()
    {
        long date_2023_04_18_12_14_00 = 1681841640000;
        WriteOrder order = new(
            new(1),
            new(date_2023_04_18_12_14_00),
            new(OperationCode.BUY),
            new("AAPL"),
            new(10),
            new(4650.89m)
        );

        BusinessErrors errors = new();
        rule.Execute(order, errors);

        errors.ToArray().Should().BeEmpty();
    }
}
