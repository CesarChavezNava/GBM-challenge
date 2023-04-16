using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;

namespace Broker.Accounts.Application.Create;

public interface IForCreateOrder
{
    Task<Account> Create(WriteOrder order);
}
