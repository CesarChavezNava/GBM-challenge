namespace Broker.Core.Exceptions;

public class SearchOffsetOutOfRangeException : ArgumentOutOfRangeException
{
    public SearchOffsetOutOfRangeException() :
        base("The search offset is out of the allowed range.")
    { }
}
