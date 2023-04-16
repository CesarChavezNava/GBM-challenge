using Broker.Core.Exceptions;

namespace Broker.Core.ValueObjects;

public class SearchLimit : ValueObject<int>
{
    public SearchLimit(int value)
        : base(value)
    { }

    protected override void Check(int value)
    {
        if (value <= 0)
            throw new SearchLimitOutOfRangeException();
    }
}
