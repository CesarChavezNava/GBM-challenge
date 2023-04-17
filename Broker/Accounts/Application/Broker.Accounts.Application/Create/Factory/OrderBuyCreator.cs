﻿using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Rules;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Core.Rules;

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
        return issuer.CreateWriteIssuerForBuyOperation(order.TotalShares, order.SharePrice);
    }

    public IBusinessRule<WriteOrder>[] GetBusinessRules(Account accountAsAdditionalData)
    {
        return new IBusinessRule<WriteOrder>[]
        {
            new ClosedMarketRule(),
            new DuplicateOperationRule(accountAsAdditionalData),
            new InsufficientBalanceRule(accountAsAdditionalData)
        };
    }
}
