namespace Broker.Core.Exceptions;

public class InvalidSearchOrderCodeException : InvalidOperationException
{
    public InvalidSearchOrderCodeException()
        : base("The search order is invalid.")
    { }
}
