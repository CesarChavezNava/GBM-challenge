using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Entities.Write;

public class WriteAccount
{
    public readonly UserId? UserId;
    public readonly Cash Cash;

    public WriteAccount(Cash cash)
    {
        Cash = cash;
    }

    public WriteAccount(UserId userId, Cash cash) 
    { 
        UserId = userId;
        Cash = cash;
    }
}
