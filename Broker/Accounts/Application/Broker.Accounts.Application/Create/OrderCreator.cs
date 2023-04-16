using Broker.Accounts.Application.Create.Factory;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Repositories;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Application.Create;

public class OrderCreator : IForCreateOrder
{
    private readonly IAccountRepository accountRepository;
    private readonly IOrderRepository orderRepository;
    public OrderCreator(
        IAccountRepository accountRepository,
        IOrderRepository orderRepository)
    {
        this.accountRepository = accountRepository;
        this.orderRepository = orderRepository;
    }

    public async Task<Account> Create(WriteOrder order)
    {
        Account account = await accountRepository.Find(order.UserId);

        await orderRepository.Create(order);

        IOrderOperationCreator operationCreator = OrderOperationCreatorFactory.Create(order.Operation);

        WriteIssuer issuer = operationCreator.CreateIssuerForCommand(EvaluateIssuer(account, order), order);
        Cash currentCash = operationCreator.CalculateCurrentCash(account.Cash, order);
        await accountRepository.SaveBalance(new(account.UserId, currentCash), issuer);


        UpdateIssuerBalanceInAccount(account, issuer);
        return account.Clone(cash: currentCash);
    }


    private WriteIssuer EvaluateIssuer(Account account, WriteOrder order)
    {
        Issuer? issuer = account.Issuers
            .FirstOrDefault(issuer => issuer.IssuerName.Value.Equals(order.IssuerName.Value));

        if (issuer is null)
        {
            return new WriteIssuer(
                new(account.UserId.Value), 
                new(order.IssuerName.Value), 
                new(order.TotalShares.Value), 
                new(order.SharePrice.Value));
        }
            

        return new WriteIssuer(
            new(account.UserId.Value), 
            new(issuer.IssuerName.Value), 
            new(issuer.TotalShares.Value), 
            new(issuer.SharesPrice.Value), true);
    }

    private void UpdateIssuerBalanceInAccount(Account account, WriteIssuer writeIssuer)
    {
        Issuer? issuer = account.Issuers
            .FirstOrDefault(issuer => issuer.IssuerName.Value.Equals(writeIssuer.IssuerName.Value));
  
        if(issuer is not null)
            account.Issuers.Remove(issuer);

        account.Issuers.Add(new Issuer(
            new(writeIssuer.IssuerName.Value), 
            new(writeIssuer.TotalShares.Value), 
            new(writeIssuer.SharePrice.Value)));
    }

}