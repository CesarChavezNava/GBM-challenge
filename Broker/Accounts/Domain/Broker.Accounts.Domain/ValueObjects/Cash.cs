using Broker.Accounts.Domain.Exceptions;
using Broker.Core.ValueObjects;

namespace Broker.Accounts.Domain.ValueObjects;

public sealed class Cash : ValueObject<decimal>
{
    public Cash(decimal value) 
        : base(Math.Floor(value * 100) / 100)
    { }

    protected override void Check(decimal value)
    {
        if (value <= 0)
            throw new InsufficientCashException();
    }
}
