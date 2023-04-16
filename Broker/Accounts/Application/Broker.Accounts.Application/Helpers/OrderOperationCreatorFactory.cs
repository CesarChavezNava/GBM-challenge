using Broker.Accounts.Application.Create;
using Broker.Accounts.Domain.Enums;

namespace Broker.Accounts.Application.Helpers;

public static class OrderOperationCreatorFactory
{
    public static IOrderOperationCreator Create(OperationCode operationCode)
    {
        string operation = operationCode.ToString().ToLower();
        string className = 
            $"Broker.Accounts.Application.Create.Order{char.ToUpper(operation[0])}{operation.Substring(1)}Creator";

        return (IOrderOperationCreator)Activator.CreateInstance(Type.GetType(className));
    }
}
