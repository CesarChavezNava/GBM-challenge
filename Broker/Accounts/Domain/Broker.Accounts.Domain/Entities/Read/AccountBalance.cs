using Broker.Core.Rules;

namespace Broker.Accounts.Domain.Entities.Read;

public class AccountBalance
{
    public readonly Balance CurrentBalance;
    public readonly BusinessErrors BusinessErrors;

    public AccountBalance(Balance currentBalance, BusinessErrors businessErrors)
    {
        CurrentBalance = currentBalance;
        BusinessErrors = businessErrors;
    }
}
