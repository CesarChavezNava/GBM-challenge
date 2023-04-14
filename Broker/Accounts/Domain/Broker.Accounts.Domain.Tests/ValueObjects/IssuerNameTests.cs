using Broker.Accounts.Domain.Exceptions;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Core.Exceptions;

namespace Broker.Accounts.Domain.Tests.ValueObjects;

[TestFixture]
public class IssuerNameTests
{
    [Test(Description = "Set empty issuer name, should return ArgumentNullOrEmptyException")]
    public void EmptyIssuerName()
    {
        Action action = () => new IssuerName("");
        action.Should().Throw<ArgumentNullOrEmptyException>();
    }

    [Test(Description = "Set issuer name with invalid format, should return InvalidStockSymbolException")]
    public void IssuerNameWithInvalidFormat() 
    {
        Action action = () => new IssuerName("GOOG$ 1");
        action.Should().Throw<InvalidStockSymbolException>();
    }

    [Test(Description = "Set issuer name with leading and trailing spaces, should return the value set above without spaces")]
    public void IssuerNameWithSpaces()
    {
        IssuerName issuerName = new(" GOOG ");
        string expected = "GOOG";

        issuerName.Should().NotBeNull();
        issuerName.Value.Should().Be(expected);
    }

    [Test(Description = "Set issuer name to lowercase, should return the value set above but in uppercase")]
    public void IssuerNameInLowercase()
    {
        IssuerName issuerName = new("tsla");
        string expected = "TSLA";

        issuerName.Should().NotBeNull();
        issuerName.Value.Should().Be(expected);
    }

}
