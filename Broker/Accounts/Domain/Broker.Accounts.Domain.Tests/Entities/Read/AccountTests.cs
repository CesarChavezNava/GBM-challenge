using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Tests.Entities.Read;

[TestFixture]
public class AccountTests
{
    private Account expected;

    [SetUp]
    public void SetUp()
    {
        expected = new(
            new UserId(1),
            new Cash(1000)
        );
    }

    [Test(Description = "Account without issuers, Should return the expected value without issuers")]
    public void ValidAccountWithoutIssuers()
    {
        Account account = new(new UserId(1), new Cash(1000));

        account.Should().NotBeNull();
        account.UserId.Value.Should().Be(expected.UserId.Value);
        account.Cash.Value.Should().Be(expected.Cash.Value);
        account.Issuers.ToArray().Should().NotBeNull().And.HaveCount(0);
    }
}
