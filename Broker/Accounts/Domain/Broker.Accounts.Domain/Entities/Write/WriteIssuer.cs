using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Entities.Write;

public class WriteIssuer
{
    public readonly UserId UserId;
    public readonly IssuerName IssuerName;
    public readonly TotalShares TotalShares;
    public readonly SharePrice SharePrice;
    public readonly bool Exists;

    public WriteIssuer(UserId userId, IssuerName issuerName, TotalShares totalShares, SharePrice sharePrice)
    {
        UserId = userId;
        IssuerName = issuerName;
        TotalShares = totalShares;
        SharePrice = sharePrice;
        Exists = false;
    }

    public WriteIssuer(UserId userId, IssuerName issuerName, TotalShares totalShares, SharePrice sharePrice, bool exists)
    {
        UserId = userId;
        IssuerName = issuerName;
        TotalShares = totalShares;
        SharePrice = sharePrice;
        this.Exists = exists;
    }

    public WriteIssuer CreateWriteIssuerForSellOperation(TotalShares totalShares, SharePrice sharePrice)
    {
        int currentTotalShares = TotalShares.Value - totalShares.Value;
        decimal averageSharePrice = (SharePrice.Value + sharePrice.Value) / currentTotalShares;

        return new WriteIssuer(
                UserId,
                IssuerName,
                new(currentTotalShares),
                new(averageSharePrice)
            );
    }

    public WriteIssuer CreateWriteIssuerForBuyOperation(TotalShares totalShares, SharePrice sharePrice)
    {
        if (!Exists)
            return this;

        int currentTotalShares = TotalShares.Value + totalShares.Value;
        decimal averageSharePrice = (SharePrice.Value + sharePrice.Value) / currentTotalShares;

        return new WriteIssuer(
                UserId,
                IssuerName,         
                new(currentTotalShares),
                new(averageSharePrice),
                this.Exists
            );
    }
}
