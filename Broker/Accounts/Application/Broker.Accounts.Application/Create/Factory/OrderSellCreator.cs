using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Application.Create.Factory;

public class OrderSellCreator : IOrderOperationCreator
{
    public Cash CalculateCurrentCash(Cash cash, WriteOrder order)
    {
        decimal currentCash = cash.Value + (order.TotalShares.Value * order.SharePrice.Value);
        return new Cash(currentCash);
    }

    public WriteIssuer CreateIssuerForCommand(WriteIssuer issuer, WriteOrder order)
    {
        return issuer.CreateWriteIssuerForSellOperation(
            new(order.TotalShares.Value),
            new(order.SharePrice.Value));
    }
}
