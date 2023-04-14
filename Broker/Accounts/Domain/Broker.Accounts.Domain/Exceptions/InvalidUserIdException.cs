namespace Broker.Accounts.Domain.Exceptions;

public class InvalidUserIdException : ArgumentException
{
    public InvalidUserIdException()
        : base("The user identifier is invalid value.") { }
}
