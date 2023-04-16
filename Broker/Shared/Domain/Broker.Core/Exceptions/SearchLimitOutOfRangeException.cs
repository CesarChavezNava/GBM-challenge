namespace Broker.Core.Exceptions;

public class SearchLimitOutOfRangeException : ArgumentOutOfRangeException
{
    public SearchLimitOutOfRangeException() :
        base("The search limit is out of the allowed range.")
    { }
}
