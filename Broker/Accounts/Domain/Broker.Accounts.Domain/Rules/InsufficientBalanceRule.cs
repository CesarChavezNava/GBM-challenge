using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Core.Rules;

namespace Broker.Accounts.Domain.Rules;

/// <summary>
/// When buying stocks, you must have enough cash in order to fulfill it.
/// </summary>
public class InsufficientBalanceRule : DependentBusinessRule<WriteOrder, Account>
{
    public InsufficientBalanceRule(Account account)
        : base("INSUFFICIENT_BALANCE", additionalData: account, contextsToExecute: new string[] { "BUY" })
    { }

    protected override bool Validate(WriteOrder order, Account account)
    {
        decimal total = order.TotalShares.Value * order.SharePrice.Value;
        return account.Cash.Value >= total;
    }
}
