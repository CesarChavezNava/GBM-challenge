using Broker.Accounts.Application.Create;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Repositories;
using Moq;

namespace Broker.Accounts.Application.Tests.Create;

[TestFixture]
public class AccountCreatorTests
{
    [Test(Description = "Create account")]
    public async Task Create()
    {
        WriteAccount writeAccount = new(new(1000));

        Mock<IAccountRepository> accountRepository = new();
        AccountCreator accountCreator = new(accountRepository.Object);

        accountRepository.Setup(a => a.Create(writeAccount)).Returns(() => Task.FromResult(new Account(new(1), new(1000))));
        Account account = await accountCreator.Create(writeAccount);

        account.UserId.Value.Should().Be(1);
        account.Cash.Value.Should().Be(1000);

        accountRepository.Setup(a => a.Create(writeAccount));
    }
}
