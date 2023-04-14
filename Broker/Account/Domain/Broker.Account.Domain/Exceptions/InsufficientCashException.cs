namespace Broker.Account.Domain.Exceptions;

public class InsufficientCashException : ArgumentException
{
    public InsufficientCashException() 
        : base("Insufficient Cash.") 
    { }
}
