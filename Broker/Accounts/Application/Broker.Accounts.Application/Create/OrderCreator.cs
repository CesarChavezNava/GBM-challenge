using Broker.Accounts.Application.Create.Factory;
using Broker.Accounts.Application.Create.Helpers;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Repositories;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Core.Rules;

namespace Broker.Accounts.Application.Create;

public class OrderCreator : IForCreateOrder
{
    private readonly IAccountRepository accountRepository;
    private readonly IOrderRepository orderRepository;
    private readonly Policy<WriteOrder> policy;
    public OrderCreator(
        IAccountRepository accountRepository,
        IOrderRepository orderRepository,
        Policy<WriteOrder> policy)
    {
        this.accountRepository = accountRepository;
        this.orderRepository = orderRepository;
        this.policy = policy;
    }

    public async Task<Account> Create(WriteOrder order)
    {
        Account account = await accountRepository.Find(order.UserId);
        IOrderOperationCreator operationCreator = OrderOperationCreatorFactory.Create(order.Operation);

        BusinessErrors businessErrors = policy.Execute(order, operationCreator.GetBusinessRules(account));
        if(businessErrors.HasErrors())
        {
            account.AddBusinessErrors(businessErrors);
            return account;
        }

        await orderRepository.Create(order);

        WriteIssuer issuerEvaluated = EvaluateIssuer(account, order);
        WriteIssuer issuer = operationCreator.CreateIssuerForCommand(issuerEvaluated, order);

        Cash currentCash = operationCreator.CalculateCurrentCash(account.Cash, order);
        await accountRepository.SaveBalance(new(account.UserId, currentCash), issuer);

        account.UpdateIssuer(Transformer.FromWriteIssuer(issuer));
        return account.Clone(cash: currentCash);
    }

    private WriteIssuer EvaluateIssuer(Account account, WriteOrder order)
    {
        Issuer? issuer = account.Issuers
            .FirstOrDefault(issuer => issuer.IssuerName.Value.Equals(order.IssuerName.Value));

        if (issuer is null)
        {
            return new WriteIssuer(
                account.UserId,
                order.IssuerName,
                order.TotalShares, 
                order.SharePrice);
        }

        return new WriteIssuer(
            account.UserId, 
            issuer.IssuerName, 
            issuer.TotalShares, 
            issuer.SharePrice, 
            exists: true);
    }
}