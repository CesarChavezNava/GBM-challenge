namespace Broker.Accounts.Domain.Exceptions;

public class TimestampOutOfRangeException : ArgumentOutOfRangeException
{
    public TimestampOutOfRangeException() :
        base("The timestamp is out of the allowed range.")
    { }
}
