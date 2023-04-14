namespace Broker.Accounts.Domain.Exceptions;

public class InsufficientSharesException : ArgumentException
{
    public InsufficientSharesException()
        : base("Insufficient shares.")
    { }
}
