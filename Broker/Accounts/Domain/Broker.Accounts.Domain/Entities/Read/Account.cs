using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Entities.Read;

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

    public Account Clone(UserId? userId = null, Cash? cash = null, Issuers? issuers = null)
    {
        return new Account(
            userId ?? this.UserId,
            cash ?? this.Cash,
            issuers ?? this.Issuers
        );
    }
}
