﻿using Broker.Accounts.Domain.ValueObjects;

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

    public WriteIssuer CreateWriteIssuerForSellOperation(TotalShares totalShares, SharePrice sharePrice)
    {
        int _totalShares = this.TotalShares.Value - totalShares.Value;
        decimal _sharePrice = (this.SharePrice.Value + sharePrice.Value) / _totalShares;

        return new WriteIssuer(
                this.UserId,
                this.IssuerName,
                new(_totalShares),
                new(_sharePrice)
            );
    }

    public WriteIssuer CreateWriteIssuerForBuyOperation(TotalShares totalShares, SharePrice sharePrice)
    {
        int _totalShares = this.TotalShares.Value + totalShares.Value;
        decimal _sharePrice = (this.SharePrice.Value + sharePrice.Value) / _totalShares;

        return new WriteIssuer(
                this.UserId,
                this.IssuerName,
                new(_totalShares),
                new(_sharePrice)
            );
    }
}
