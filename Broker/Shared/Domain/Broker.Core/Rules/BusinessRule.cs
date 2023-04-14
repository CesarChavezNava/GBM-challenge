namespace Broker.Core.Rules;

/// <summary>
/// Base contract to generate a rule.
/// </summary>
/// <typeparam name="TInput">Type of entity that will pass through the rule.</typeparam>
public interface IBusinessRule<TInput>
{
    /// <summary>
    /// Array of contexts under which the rule can be executed.
    /// Example: ["SELL"] This means that it can be executed in the sale context.
    /// </summary>
    string[]? ContextsToExecute { get; set; }

    void Execute(TInput entity, BusinessErrors errors);
}

/// <summary>
/// Subcontract for creating rules that do not require additional data.
/// </summary>
/// <typeparam name="TInput">Type of entity that will go through the rule.</typeparam>
public abstract class BusinessRule<TInput> : IBusinessRule<TInput>
{
    private readonly string businessErrorCode;
    public string[]? ContextsToExecute { get; set; }

    public BusinessRule(string businessErrorCode)
    {
        this.businessErrorCode = businessErrorCode;
    }

    public BusinessRule(string businessErrorCode, string[] contextsToExecute)
    {
        this.businessErrorCode = businessErrorCode;
        this.ContextsToExecute = contextsToExecute;
    }

    public void Execute(TInput entity, BusinessErrors errors)
    {
        bool isValid = Validate(entity);
        if(!isValid) 
            errors.Add(businessErrorCode);
    }

    /// <summary>
    /// Validate that entity meets the requirements.
    /// </summary>
    /// <param name="entity">Entity to validate.</param>
    /// <returns>If It's valid or not.</returns>
    protected abstract bool Validate(TInput entity);
}
