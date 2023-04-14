using Broker.Account.Domain.Exceptions;
using Broker.Core.ValueObjects;

namespace Broker.Account.Domain.ValueObjects;

public sealed class Cash : ValueObject<decimal>
{
    public Cash(decimal value) 
        : base(value)
    { }

    protected override void Check(decimal value)
    {
        if (value <= 0)
            throw new InsufficientCashException();
    }
}
