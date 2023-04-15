using Broker.Accounts.Domain.Exceptions;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Tests.ValueObjects;

[TestFixture]
public class TimestampTests
{
    private Timestamp expected;

    [SetUp]
    public void SetUp()
    {
        long date_2023_04_18_12_00_00 = 1681840800000;
        expected = new(date_2023_04_18_12_00_00);
    }


    [Test(Description = "Set a negative timestamp, should return TimestampOutOfRangeException")]
    public void NegativeTimestamp()
    {
        Action action = () => new Timestamp(-1681815600000);
        action.Should().Throw<TimestampOutOfRangeException>();
    }

    [Test(Description = "Set a timestamp in 0, should return TimestampOutOfRangeException")]
    public void TimestampAs0()
    {
        Action action = () => new Timestamp(0);
        action.Should().Throw<TimestampOutOfRangeException>();
    }

    [Test(Description = "Set timestamp in TIME, should return expected")]
    public void TimestampInTime()
    {
        Timestamp timestamp = new(1681840800000);
        timestamp.Should().NotBeNull();
        timestamp.Value.Should().Be(expected.Value);
    }
}
