using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Rules;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Core.Rules;

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
        return issuer.CreateWriteIssuerForSellOperation(order.TotalShares, order.SharePrice);    
    }

    public IBusinessRule<WriteOrder>[] GetBusinessRules(Account accountAsAdditionalData)
    {
        return new IBusinessRule<WriteOrder>[]
        {
            new ClosedMarketRule(),
            new InsufficientStocksRule(accountAsAdditionalData)
        };
    }
}
