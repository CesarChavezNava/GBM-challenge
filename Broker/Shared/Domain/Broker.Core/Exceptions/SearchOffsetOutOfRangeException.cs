namespace Broker.Core.Exceptions;

public class SearchOffsetOutOfRangeException : ArgumentOutOfRangeException
{
    public SearchOffsetOutOfRangeException() :
        base("Search offset out of allowed range.")
    { }
}
