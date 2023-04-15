using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Enums;
using Broker.Core.Rules;

namespace Broker.Accounts.Domain.Rules;

public class OrdersPolicy : Policy<WriteOrder>
{
    protected override bool ValidateContexts(WriteOrder order, IBusinessRule<WriteOrder> rule)
    {
        if (rule.ContextsToExecute is null)
            return true;

        foreach (string context in rule.ContextsToExecute)
        {
            OperationCode currentContext = (OperationCode)Enum.Parse(typeof(OperationCode), context);
            if (order.Operation.Value != currentContext)
                return false;
        }

        return true;
    }
}
