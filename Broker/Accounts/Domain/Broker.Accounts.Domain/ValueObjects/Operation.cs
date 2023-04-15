using Broker.Accounts.Domain.Enums;
using Broker.Accounts.Domain.Exceptions;
using Broker.Core.ValueObjects;

namespace Broker.Accounts.Domain.ValueObjects;

public class Operation : ValueObject<OperationCode>
{
    public Operation(OperationCode value)
        : base(value)
    { }

    protected override void Check(OperationCode value)
    {
        if (!Enum.IsDefined(typeof(OperationCode), value))
            throw new InvalidOrderOperationException();
    }
}
