namespace Broker.Accounts.Domain.Exceptions;

public class UserNotFoundException : MissingFieldException
{
    public UserNotFoundException()
        : base("User not found")
    { }
}
