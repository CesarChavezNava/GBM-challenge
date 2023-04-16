using Broker.Accounts.Application.Helpers;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Repositories;
using Broker.Accounts.Domain.ValueObjects;

namespace Broker.Accounts.Application.Create;

public interface IOrderOperationCreator
{
    Cash CalculateBalanceCash(Cash cash, WriteOrder order);
    WriteIssuer CreateWriteIssuerForOperation(WriteIssuer issuer, WriteOrder order);
    Task SaveIssuerBalance(IIssuerRepository issuerRepository, WriteIssuer issuer);
}

public class OrderCreator : IForCreateOrder
{
    private readonly IAccountRepository accountRepository;
    private readonly IIssuerRepository issuerRepository;
    private readonly IOrderRepository orderRepository;
    public OrderCreator(
        IAccountRepository accountRepository,
        IIssuerRepository issuerRepository,
        IOrderRepository orderRepository)
    {
        this.accountRepository = accountRepository;
        this.issuerRepository = issuerRepository;
        this.orderRepository = orderRepository;
    }

    public async Task<Account> Create(WriteOrder order)
    {
        Account account = await accountRepository.Find(order.UserId);

        IOrderOperationCreator orderOperationCreator = OrderOperationCreatorFactory.Create(order.Operation.Value);

        WriteIssuer issuer = orderOperationCreator.CreateWriteIssuerForOperation(
            CreateNewOrGetIssuer(account, order), order);

        await orderOperationCreator.SaveIssuerBalance(issuerRepository, issuer);
        await orderRepository.Create(order);

        Cash currentCash = orderOperationCreator.CalculateBalanceCash(account.Cash, order);
        await accountRepository.Update(
            new WriteAccount(new UserId(account.UserId.Value), currentCash));

        UpdateIssuerBalanceInAccount(account, issuer);
        return account.Clone(cash: currentCash);
    }

    private WriteIssuer CreateNewOrGetIssuer(Account account, WriteOrder order)
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