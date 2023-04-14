using Broker.Account.Domain.Exceptions;
using Broker.Core.ValueObjects;

namespace Broker.Account.Domain.ValueObjects;

public class SharePrice : ValueObject<decimal>
{
    public SharePrice(decimal value)
        : base(value)
    { }

    protected override void Check(decimal value)
    {
        if (value < 0)
            throw new TooLowSharePriceException();
    }
}
