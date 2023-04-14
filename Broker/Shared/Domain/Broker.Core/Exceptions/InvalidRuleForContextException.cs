namespace Broker.Core.Exceptions;

public class InvalidRuleForContextException : InvalidOperationException
{
    public InvalidRuleForContextException(string ruleName)
        : base ($"The rule {ruleName} can't be executed for the current context.")
    { }
}
