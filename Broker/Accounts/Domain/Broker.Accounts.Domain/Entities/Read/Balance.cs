using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Entities.Read;

public class Balance
{
    public readonly Cash Cash;
    public readonly Issuers Issuers;

    public Balance(Cash cash, Issuers issuers)
    {
        Cash = cash;
        Issuers = issuers;
    }
}
