using Broker.Account.Domain.Exceptions;
using Broker.Core.ValueObjects;

namespace Broker.Account.Domain.ValueObjects;

public sealed class UserId : ValueObject<int> 
{
    public UserId(int value)
        : base(value)
    { }

    protected override void Check(int value)
    {
        if (value <= 0)
            throw new InvalidUserIdException();
    }
}
