namespace Broker.Core.Exceptions;

internal class MinutesOutOfRangeException : ArgumentOutOfRangeException
{
    public MinutesOutOfRangeException() :
        base("Minutes are out of range.")
    { }
}
