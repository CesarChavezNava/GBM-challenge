using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Repositories;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Application.Create;

public class OrderBuyCreator : IOrderOperationCreator
{
    public Cash CalculateBalanceCash(Cash cash, WriteOrder order)
    {
        decimal currentCash = cash.Value - (order.TotalShares.Value * order.SharePrice.Value);
        return new Cash(currentCash);
    }

    public WriteIssuer CreateWriteIssuerForOperation(WriteIssuer issuer, WriteOrder order)
    {
        return issuer.CreateWriteIssuerForBuyOperation(
            new(order.TotalShares.Value), 
            new(order.SharePrice.Value));
    }

    public async Task SaveIssuerBalance(IIssuerRepository issuerRepository, WriteIssuer issuer)
    {
        if (issuer.Exists)
            await issuerRepository.Update(issuer);
        else
            await issuerRepository.Create(issuer);
    }
}
