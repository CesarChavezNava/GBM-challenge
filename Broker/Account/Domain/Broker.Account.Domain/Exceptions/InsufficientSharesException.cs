namespace Broker.Account.Domain.Exceptions;

public class InsufficientSharesException : ArgumentException
{
    public InsufficientSharesException()
        : base("Insufficient shares.")
    { }
}
