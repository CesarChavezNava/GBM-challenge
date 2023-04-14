using Broker.Accounts.Domain.Exceptions;
using Broker.Core.ValueObjects;

namespace Broker.Accounts.Domain.ValueObjects;

public sealed class TotalShares : ValueObject<int>
{
    public TotalShares(int value)
        : base(value)
    { }

    protected override void Check(int value)
    {
        if (value <= 0)
            throw new InsufficientSharesException();
    }
}
