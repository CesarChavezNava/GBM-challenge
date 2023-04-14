namespace Broker.Core.Exceptions;

public class ArgumentNullOrEmptyException : ArgumentException
{
    public ArgumentNullOrEmptyException(string argumentName)
        : base($"The {argumentName} is null or empty.")
    { }
}
