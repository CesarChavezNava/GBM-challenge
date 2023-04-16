using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Core.Rules;

namespace Broker.Accounts.Domain.Rules;

/// <summary>
/// When selling stocks, you must have enough stocks in order to fulfill it
/// </summary>
public class InsufficientStocksRule : DependentBusinessRule<WriteOrder, Account>
{
	public InsufficientStocksRule(Account account)
		: base("INSUFFICIENT_STOCKS", additionalData: account, contextsToExecute: new string[] { "SELL" })
	{ }

    protected override bool Validate(WriteOrder order, Account account)
    {
        Issuer? issuer =
            account.Issuers.FirstOrDefault(issuer => issuer.IssuerName.Value.Equals(order.IssuerName.Value));

        if (issuer is null)
            return false;

        return issuer.TotalShares.Value >= order.TotalShares.Value;
    }
}
