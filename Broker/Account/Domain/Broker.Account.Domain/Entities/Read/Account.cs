using Broker.Account.Domain.ValueObjects;

namespace Broker.Account.Domain.Entities.Read;

public class Account
{
    public readonly UserId UserId;
    public readonly Cash Cash;
    public readonly Issuers Issuers;

    public Account(UserId userId, Cash cash)
    {
        UserId = userId;
        Cash = cash;
        Issuers = new Issuers();
    }

    public Account(UserId userId, Cash cash, Issuers issuers)
    {
        UserId = userId;
        Cash = cash;
        Issuers = issuers;
    }
}
