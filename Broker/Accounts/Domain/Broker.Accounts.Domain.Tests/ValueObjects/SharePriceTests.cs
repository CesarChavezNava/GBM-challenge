using Broker.Accounts.Domain.Exceptions;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Tests.ValueObjects;

[TestFixture]
public class SharePriceTests
{
    [Test(Description = "Set negative share price, should return TooLowSharePriceException")]
    public void NegativeSharePrice()
    {
        Action action = () => new SharePrice(-3400.98m);
        action.Should().Throw<TooLowSharePriceException>();
    }

    [Test(Description = "Set share price to 0, should return TooLowSharePriceException")]
    public void InsufficientSharePrice()
    {
        Action action = () => new SharePrice(0);
        action.Should().Throw<TooLowSharePriceException>();
    }

    [Test(Description = "Set positive share price, should return the value set above")]
    public void PositiveSharePrice()
    {
        decimal value = 3456.76m;
        SharePrice sharePrice = new(value);

        sharePrice.Should().NotBeNull();
        sharePrice.Value.Should().Be(value);
    }

    [Test(Description = "Set positive share price with 5 decimals, should return a value close to the value set above with 2 decimal places")]
    public void PositiveSharePriceWith5DecimalRoundUp()
    {
        decimal value = 3456.76983m;
        decimal expected = 3456.76m;
        SharePrice sharePrice = new(value);

        sharePrice.Should().NotBeNull();
        sharePrice.Value.Should().Be(expected);
    }

    [Test(Description = "Set positive share price with 5 decimals, should return a value close to the value set above with 2 decimal places")]
    public void PositiveSharePriceWith5DecimalRoundDown()
    {
        decimal value = 3456.7633m;
        decimal expected = 3456.76m;
        SharePrice sharePrice = new(value);

        sharePrice.Should().NotBeNull();
        sharePrice.Value.Should().Be(expected);
    }
}
