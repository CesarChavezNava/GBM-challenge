using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Repositories;

public interface IAccountRepository
{
    Task<Account> Create(WriteAccount account);
    Task<Account> Find(UserId userId);
    Task<Account> Update(WriteAccount account);
}
