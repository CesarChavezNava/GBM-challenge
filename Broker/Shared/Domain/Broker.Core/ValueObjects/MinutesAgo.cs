using Broker.Core.Exceptions;

namespace Broker.Core.ValueObjects;

public class MinutesAgo : ValueObject<int>
{
    public MinutesAgo(int value)
        : base(value)
    { }

    protected override void Check(int value)
    {
        if(value <= 0)
            throw new MinutesOutOfRangeException();
    }

    public long ToMiliseconds()
    {
        return Value * 60 * 1000;
    }

    public long Subtraction(long timestampValue)
    {
        return timestampValue - ToMiliseconds();
    }
}
