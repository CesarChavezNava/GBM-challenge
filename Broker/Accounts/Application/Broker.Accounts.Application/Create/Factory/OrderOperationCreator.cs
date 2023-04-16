using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Core.Rules;

namespace Broker.Accounts.Application.Create.Factory;

public interface IOrderOperationCreator
{
    Cash CalculateCurrentCash(Cash prevCash, WriteOrder order);
    WriteIssuer CreateIssuerForCommand(WriteIssuer issuer, WriteOrder order);
    IBusinessRule<WriteOrder>[] GetBusinessRules(Account accountAsAdditionalData);
}