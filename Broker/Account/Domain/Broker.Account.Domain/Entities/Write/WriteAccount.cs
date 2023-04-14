using Broker.Account.Domain.ValueObjects;

namespace Broker.Account.Domain.Entities.Write;

public class WriteAccount
{
    public readonly Cash Cash;

    public WriteAccount(Cash cash)
    {
        Cash = cash;
    }
}
