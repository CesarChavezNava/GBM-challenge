using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Entities.Read;

public class Order
{
    public readonly Timestamp Timestamp;
    public readonly Operation Operation;
    public readonly IssuerName IssuerName;
    public readonly TotalShares TotalShares;
    public readonly SharePrice SharePrice;

    public Order(Timestamp timestamp, Operation operation, IssuerName issuerName, TotalShares totalShares, SharePrice sharePrice)
    {
        Timestamp = timestamp;
        Operation = operation;
        IssuerName = issuerName;
        TotalShares = totalShares;
        SharePrice = sharePrice;
    }
}
