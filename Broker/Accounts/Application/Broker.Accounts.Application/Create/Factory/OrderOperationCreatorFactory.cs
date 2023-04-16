using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Application.Create.Factory;

public static class OrderOperationCreatorFactory
{
    public const string NAMESPACE = "Broker.Accounts.Application.Create.Factory";

    public static IOrderOperationCreator Create(Operation operation)
    {
        string operationCode = operation.Value.ToString().ToLower();
        string className = $"Order{char.ToUpper(operationCode[0])}{operationCode.Substring(1)}Creator";
        string fullyQualifiedName = $"{NAMESPACE}.{className}";

        return (IOrderOperationCreator)Activator.CreateInstance(Type.GetType(fullyQualifiedName));
    }
}
