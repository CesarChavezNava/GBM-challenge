using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Entities.Write;

public class WriteOrder
{
    public readonly UserId UserId;
    public readonly Timestamp Timestamp;
    public readonly Operation Operation;
    public readonly IssuerName IssuerName;
    public readonly TotalShares TotalShares;
    public readonly SharePrice SharePrice;

    public WriteOrder(
        UserId userId, 
        Timestamp timestamp, 
        Operation operation, 
        IssuerName issuerName, 
        TotalShares totalShares, 
        SharePrice sharePrice)
    {
        UserId = userId;
        Timestamp = timestamp;
        Operation = operation;
        IssuerName = issuerName;
        TotalShares = totalShares;
        SharePrice = sharePrice;
    }
}
