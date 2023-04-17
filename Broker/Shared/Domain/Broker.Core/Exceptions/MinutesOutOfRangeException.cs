namespace Broker.Core.Exceptions;

public class MinutesOutOfRangeException : ArgumentOutOfRangeException
{
    public MinutesOutOfRangeException() :
        base("Minutes out of range.")
    { }
}
