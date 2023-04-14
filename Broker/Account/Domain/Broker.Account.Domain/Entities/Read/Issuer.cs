using Broker.Account.Domain.ValueObjects;

namespace Broker.Account.Domain.Entities.Read;

public class Issuer
{
    public readonly IssuerName IssuerName;
    public readonly TotalShares TotalShares;
    public readonly SharePrice SharesPrice;

    public Issuer(IssuerName issuerName, TotalShares totalShares, SharePrice sharePrice)
    {
        IssuerName = issuerName;
        TotalShares = totalShares;
        SharesPrice = sharePrice;
    }
}
