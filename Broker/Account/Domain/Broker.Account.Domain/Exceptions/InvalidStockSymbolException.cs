namespace Broker.Account.Domain.Exceptions;

public class InvalidStockSymbolException : ArgumentException
{
    public InvalidStockSymbolException()
        : base("Invalidly formatted stock symbol.")
    { }
}
