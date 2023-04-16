using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Application.Create.Factory;

public class OrderBuyCreator : IOrderOperationCreator
{
    public Cash CalculateCurrentCash(Cash prevCash, WriteOrder order)
    {
        decimal currentCash = prevCash.Value - (order.TotalShares.Value * order.SharePrice.Value);
        return new Cash(currentCash);
    }

    public WriteIssuer CreateIssuerForCommand(WriteIssuer issuer, WriteOrder order)
    {
        return issuer.CreateWriteIssuerForBuyOperation(
            new(order.TotalShares.Value),
            new(order.SharePrice.Value));
    }
}
