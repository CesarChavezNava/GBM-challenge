using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Application.Create.Factory;

public interface IOrderOperationCreator
{
    Cash CalculateCurrentCash(Cash prevCash, WriteOrder order);
    WriteIssuer CreateIssuerForCommand(WriteIssuer issuer, WriteOrder order);
}