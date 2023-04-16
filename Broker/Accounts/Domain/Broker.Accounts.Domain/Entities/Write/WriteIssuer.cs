using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Entities.Write;

public class WriteIssuer
{
    public readonly UserId UserId;
    public readonly IssuerName IssuerName;
    public readonly TotalShares TotalShares;
    public readonly SharePrice SharePrice;

    public WriteIssuer(UserId userId, IssuerName issuerName, TotalShares totalShares, SharePrice sharePrice)
    {
        UserId = userId;
        IssuerName = issuerName;
        TotalShares = totalShares;
        SharePrice = sharePrice;
    }
}
