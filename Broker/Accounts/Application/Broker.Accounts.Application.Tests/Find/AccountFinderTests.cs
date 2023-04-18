using Broker.Accounts.Application.Find;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Repositories;
using Broker.Accounts.Domain.ValueObjects;
using Moq;

namespace Broker.Accounts.Application.Tests.Find;

[TestFixture]
public class AccountFinderTests
{
    [Test(Description = "Find account")]
    public async Task Find()
    {
        UserId userId = new(1);

        Mock<IAccountRepository> accountRepository = new();
        AccountFinder accountFinder = new(accountRepository.Object);

        accountRepository.Setup(a => a.Find(It.IsAny<UserId>())).Returns(() => Task.FromResult(new Account(new(1), new(1000))));
        Account account = await accountFinder.Find(userId);

        account.UserId.Value.Should().Be(1);
        account.Cash.Value.Should().Be(1000);

        accountRepository.Setup(a => a.Find(It.IsAny<UserId>()));
    }
}
