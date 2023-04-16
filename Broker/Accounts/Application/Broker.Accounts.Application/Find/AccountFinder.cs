using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Repositories;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Application.Find;

public class AccountFinder : IForFindAccount
{
    private readonly IAccountRepository accountRepository;
    public AccountFinder(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }

    public async Task<Account> Find(UserId userId)
    {
        return await accountRepository.Find(userId);
    }
}
