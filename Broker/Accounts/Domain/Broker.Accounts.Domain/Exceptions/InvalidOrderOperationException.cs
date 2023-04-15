namespace Broker.Accounts.Domain.Exceptions;

public class InvalidOrderOperationException : InvalidOperationException
{
    public InvalidOrderOperationException()
        : base("The order operaion is invalid.")
    { }
}
