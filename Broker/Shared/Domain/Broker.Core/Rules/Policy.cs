using Broker.Core.Exceptions;

namespace Broker.Core.Rules;

/// <summary>
/// Base contract to generate a policy.
/// </summary>
/// <typeparam name="TInput">Type of entity that will pass through the policy.</typeparam>
public abstract class Policy<TInput>
{
    public BusinessErrors Execute(TInput entity, IBusinessRule<TInput>[] rules)
    {
        bool canBeExecuted = true;
        BusinessErrors errors = new();
        
        foreach (var rule in rules) 
        {
            if (rule.ContextsToExecute is not null)
                canBeExecuted = ValidateContexts(entity, rule);

            if (!canBeExecuted)
                throw new InvalidRuleForContextException(rule.GetType().Name);

            rule.Execute(entity, errors);
        }

        return errors;
    }

    /// <summary>
    /// Method that validates the contexts under which a rule can be executed
    /// </summary>
    /// <param name="entity">Validation target entity</param>
    /// <param name="rule">Rule to be validated</param>
    /// <returns></returns>
    protected abstract bool ValidateContexts(TInput entity, IBusinessRule<TInput> rule);
}
