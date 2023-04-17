using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Core.Rules;

namespace Broker.Accounts.Domain.Rules;

public class DuplicateOperationRule : DependentBusinessRule<WriteOrder, Account>
{
    private const long MINUTES_5 = 300000;
    public DuplicateOperationRule(Account account)
        : base("DUPLICATE_OPERATION", additionalData: account)
    { }

    protected override bool Validate(WriteOrder order, Account account)
    {
        Timestamp timestamp = new(order.Timestamp.Value - MINUTES_5);

        return !account.Orders.Any(o =>
                    o.IssuerName.Value.Equals(order.IssuerName.Value) &&
                    o.TotalShares.Value == order.TotalShares.Value &&
                    o.SharePrice.Value == order.SharePrice.Value &&
                    o.Timestamp.Value >= timestamp.Value);
    }
}
