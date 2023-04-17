namespace Broker.Core.Exceptions;

public class SearchLimitOutOfRangeException : ArgumentOutOfRangeException
{
    public SearchLimitOutOfRangeException() :
        base("Search limit out of allowed range.")
    { }
}
