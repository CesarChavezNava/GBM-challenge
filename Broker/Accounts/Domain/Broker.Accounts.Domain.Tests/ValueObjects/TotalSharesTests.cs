using Broker.Accounts.Domain.Exceptions;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Tests.ValueObjects;

[TestFixture]
public class TotalSharesTests
{
    [Test(Description = "Set negative total shares, should return InsufficientSharesException")]
    public void NegativeTotalShares()
    {
        Action action = () => new TotalShares(-10);
        action.Should().Throw<InsufficientSharesException>();
    }

    [Test(Description = "Set total shares to 0, should return InsufficientSharesException")]
    public void InsufficientTotalShares()
    {
        Action action = () => new TotalShares(0);
        action.Should().Throw<InsufficientSharesException>();
    }

    [Test(Description = "Set positive total shares, should return the value set above")]
    public void PositiveTotalShares()
    {
        int value = 10;
        TotalShares totalShares = new(value);

        totalShares.Should().NotBeNull();
        totalShares.Value.Should().Be(value);
    }
}
