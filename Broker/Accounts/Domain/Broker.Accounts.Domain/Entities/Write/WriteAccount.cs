using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Domain.Entities.Write;

public class WriteAccount
{
    public readonly Cash Cash;

    public WriteAccount(Cash cash)
    {
        Cash = cash;
    }
}
