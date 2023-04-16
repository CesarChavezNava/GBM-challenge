using Broker.Accounts.Domain.Enums;
using Broker.Accounts.Domain.Exceptions;
using Broker.Core.ValueObjects;

namespace Broker.Accounts.Domain.ValueObjects;

public class Operation : ValueObjectForEnum<OperationCode>
{
    public Operation(string value)
        : base(value, true)
    { }

    public Operation(OperationCode value)
        : base(value)    
    { }

    protected override void Check(string value)
    {
        if (!Enum.IsDefined(typeof(OperationCode), value))
            throw new InvalidOrderOperationException();
    }
}
