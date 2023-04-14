using Broker.Account.Domain.Exceptions;
using Broker.Core.Exceptions;
using Broker.Core.ValueObjects;
using System.Text.RegularExpressions;

namespace Broker.Account.Domain.ValueObjects;

public sealed class IssuerName : ValueObject<string>
{
    public IssuerName(string value) 
        : base(value.Trim().ToUpper())
    { }

    protected override void Check(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentNullOrEmptyException("Issuer Name");

        Regex regex = new(@"^[A-Z]+\d*([A-Z]*)$");
        if (!regex.IsMatch(value))
            throw new InvalidStockSymbolException();
    }
}
