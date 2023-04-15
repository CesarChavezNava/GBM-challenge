using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Core.Rules;

namespace Broker.Accounts.Domain.Rules;

/// <summary>
/// When buying stocks, you must have enough cash in order to fulfill it.
/// </summary>
public class InsufficientBalanceRule : DependentBusinessRule<WriteOrder, Balance>
{
    public InsufficientBalanceRule(Balance currentBalance)
        : base("INSUFFICIENT_BALANCE", additionalData: currentBalance, contextsToExecute: new string[] { "BUY" })
    { }

    protected override bool Validate(WriteOrder order, Balance currentBalance)
    {
        decimal total = order.TotalShares.Value * order.SharePrice.Value;
        return currentBalance.Cash.Value >= total;
    }
}
