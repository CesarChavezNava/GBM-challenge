using Broker.Accounts.Domain.Exceptions;
using Broker.Core.ValueObjects;

namespace Broker.Accounts.Domain.ValueObjects;

public class Timestamp : ValueObject<long>
{
    public Timestamp(long value)
        : base(value)
    { }

    protected override void Check(long value)
    {
        if (value <= 0)
            throw new TimestampOutOfRangeException();
    }
}
