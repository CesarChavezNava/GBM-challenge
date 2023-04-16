using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Entities.Read;

public class Issuer
{
    public readonly IssuerName IssuerName;
    public readonly TotalShares TotalShares;
    public readonly SharePrice SharePrice;

    public Issuer(IssuerName issuerName, TotalShares totalShares, SharePrice sharePrice)
    {
        IssuerName = issuerName;
        TotalShares = totalShares;
        SharePrice = sharePrice;
    }
}
