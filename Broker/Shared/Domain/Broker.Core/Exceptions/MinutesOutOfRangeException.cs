namespace Broker.Core.Exceptions;

public class MinutesOutOfRangeException : ArgumentOutOfRangeException
{
    public MinutesOutOfRangeException() :
        base("Minutes are out of range.")
    { }
}
