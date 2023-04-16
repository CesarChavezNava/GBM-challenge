using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Application.Find;

public interface IForFindAccount
{
    Task<Account> Find(UserId userId); 
}
