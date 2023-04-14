using Broker.Accounts.Domain.Exceptions;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Tests.ValueObjects;

[TestFixture]
public class CashTests
{
    [Test(Description = "Set negative cash, should return InsufficientCashException")]
    public void NegativeCash()
    {
        Action action = () => new Cash(-10);
        action.Should().Throw<InsufficientCashException>();
    }

    [Test(Description = "Set cash to 0, should return InsufficientCashException")]
    public void InsufficientCash()
    {
        Action action = () => new Cash(0);
        action.Should().Throw<InsufficientCashException>();
    }

    [Test(Description = "Set positive cash, should return the value set above")]
    public void PositiveCash()
    {
        decimal value = 999.99m;
        Cash cash = new(value);

        cash.Should().NotBeNull();
        cash.Value.Should().Be(value);
    }

    [Test(Description = "Set positive cash with 5 decimals, should return the value set above but rounded to 2 decimals")]
    public void PositiveCashWith5Decimal()
    {
        decimal value = 978.98723m;
        decimal expected = 978.98m;
        Cash cash = new(value);

        cash.Should().NotBeNull();
        cash.Value.Should().Be(expected);
    }
}
