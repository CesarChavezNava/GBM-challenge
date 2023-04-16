using Broker.Core.Exceptions;

namespace Broker.Core.ValueObjects;

public class SearchOffset : ValueObject<int>
{
    public SearchOffset(int value)
        : base(value)
    { }

    protected override void Check(int value)
    {
        if (value <= 0)
            throw new SearchOffsetOutOfRangeException();
    }
}
