using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Exceptions;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Tests.Entities.Read;

[TestFixture]
public class IssuerTests
{
    private Issuer expected;
    private Issuers issuers;

    [SetUp]
    public void SetUp()
    {
        expected = new(
            new IssuerName("MSFT"),
            new TotalShares(10),
            new SharePrice(5137.21m)
        );

        issuers = new();
    }

    [Test(Description = "Invalid Issuer because the issuer name is invalid stock symbol, should return InvalidStockSymbolException")]
    public void IssuerNameWithInvalidStockSymbol()
    {
        Action action = () => new Issuer(
            new IssuerName("M$FT"),
            new TotalShares(10),
            new SharePrice(5137.21m)
        );

        action.Should().Throw<InvalidStockSymbolException>();
    }

    [Test(Description = "Valid issuer, should return expected issuer")]
    public void ValidIssuer()
    {
        Issuer issuer = new(
            new IssuerName("MSFT"),
            new TotalShares(10),
            new SharePrice(5137.21m)
        );
        
        issuer.Should().NotBeNull();
        issuer.IssuerName.Value.Should().Be(expected.IssuerName.Value);
        issuer.TotalShares.Value.Should().Be(expected.TotalShares.Value);
        issuer.SharePrice.Value.Should().Be(expected.SharePrice.Value);
    }

    [Test(Description = "Add a issuer to the issuers collection, the first element of the collection should be the same as expected")]
    public void AddIssuerIntoIssuers()
    {
        Issuer issuer = new(
            new IssuerName("MSFT"),
            new TotalShares(10),
            new SharePrice(5137.21m)
        );

        issuers.Add(issuer);

        issuers.ToArray().Should().NotBeNullOrEmpty().And.HaveCount(1);
        issuers.ToArray().SingleOrDefault().Should().NotBeNull();
        issuers.ToArray().SingleOrDefault()?.IssuerName.Value.Should().Be(expected.IssuerName.Value);
        issuers.ToArray().SingleOrDefault()?.TotalShares.Value.Should().Be(expected.TotalShares.Value);
        issuers.ToArray().SingleOrDefault()?.SharePrice.Value.Should().Be(expected.SharePrice.Value);
    }

    [Test(Description = "Empty issuers collection, should be empty array")]
    public void EmptyIssuers()
    {
        Issuers issuers = new();
        issuers.ToArray().Should().NotBeNull().And.HaveCount(0);
    }
}
