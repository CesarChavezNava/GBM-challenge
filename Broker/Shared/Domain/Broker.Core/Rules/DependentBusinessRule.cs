namespace Broker.Core.Rules;

/// <summary>
/// Subcontract for creating rules that require additional data.
/// </summary>
/// <typeparam name="TInput">Type of entity that will go through the rule.</typeparam>
/// /// <typeparam name="TAdditionalData">Type of additional data.</typeparam>
public abstract class DependentBusinessRule<TInput, TAdditionalData> : IBusinessRule<TInput>
{
    private readonly string businessErrorCode;
    private readonly TAdditionalData additionalData;
    public string[]? ContextsToExecute { get; set; }

    public DependentBusinessRule(string businessErrorCode, TAdditionalData additionalData)
    {
        this.businessErrorCode = businessErrorCode;
        this.additionalData = additionalData;
    }

    public DependentBusinessRule(string businessErrorCode, TAdditionalData additionalData, string[] contextsToExecute)
    {
        this.businessErrorCode = businessErrorCode;
        this.additionalData = additionalData;
        ContextsToExecute = contextsToExecute;
    }

    public void Execute(TInput entity, BusinessErrors errors) 
    {
        bool isValid = Validate(entity, additionalData);
        if (!isValid)
            errors.Add(businessErrorCode);
    }

    /// <summary>
    /// Validate that entity meets the requirements.
    /// </summary>
    /// <param name="entity">Entity to validate.</param>
    /// <param name="additionalData">Additional data.</param>
    /// <returns>If It's valid or not.</returns>
    protected abstract bool Validate(TInput entity, TAdditionalData additionalData);

}
