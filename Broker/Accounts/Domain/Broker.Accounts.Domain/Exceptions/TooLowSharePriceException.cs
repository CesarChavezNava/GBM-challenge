namespace Broker.Accounts.Domain.Exceptions;

public class TooLowSharePriceException : ArgumentException
{
    public TooLowSharePriceException()
        : base("The share price is too low")
    { }
}
