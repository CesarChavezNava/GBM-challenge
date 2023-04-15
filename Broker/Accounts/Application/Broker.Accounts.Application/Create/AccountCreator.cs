using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Repositories;

namespace Broker.Accounts.Application.Create;

public class AccountCreator : IForCreateAccount
{
    private readonly IAccountRepository accountRepository;

    public AccountCreator(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }

    public async Task<Account> Create(WriteAccount account)
    {
        return await accountRepository.Create(account);
    }
}
