using Broker.Accounts.Domain.Exceptions;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Tests.ValueObjects;

[TestFixture]
public class UserIdTests
{
    [Test(Description = "Set negative user id, should return InvalidUserIdException")]
    public void NegativeUserId()
    {
        Action action = () => new UserId(-1);
        action.Should().Throw<InvalidUserIdException>();
    }

    [Test(Description = "Set user id to 0, should return InvalidUserIdException")]
    public void UserIdAs0()
    {
        Action action = () => new UserId(0);
        action.Should().Throw<InvalidUserIdException>();
    }

    [Test(Description = "Set valid user id, should return the value set above")]
    public void ValidUserId()
    {
        int value = 5;
        UserId userId = new(value);

        userId.Should().NotBeNull();
        userId.Value.Should().Be(value);
    }
}
