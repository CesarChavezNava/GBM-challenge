using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Exceptions;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Tests.Entities.Write;

[TestFixture]
public class WriteAccountTests
{
    [Test(Description = "Invalid WriteAccount because the cash is 0, should return InsufficientCashException")]
    public void WriteAccountWithCash0()
    {
        Action action = () => new WriteAccount(new Cash(0));
        action.Should().Throw<InsufficientCashException>();
    }

    [Test(Description = "Valid WriteAccount, should return expected account")]
    public void ValidWriteAccount()
    {
        WriteAccount account = new(new Cash(1998.78978m));
        WriteAccount expected = new(new Cash(1998.78m));
        
        account.Cash.Value.Should().Be(expected.Cash.Value);
    }
}
