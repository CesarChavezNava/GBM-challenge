using Broker.Accounts.Domain.ValueObjects;
using Broker.Core.Rules;

namespace Broker.Accounts.Domain.Entities.Read;

public class Account
{
    public readonly UserId UserId;
    public readonly Cash Cash;
    public readonly Issuers Issuers;

    public BusinessErrors? BusinessErrors { get; private set; }

    public Account(UserId userId, Cash cash)
    {
        UserId = userId;
        Cash = cash;
        Issuers = new();
        BusinessErrors = new();
    }

    public Account(UserId userId, Cash cash, Issuers issuers)
    {
        UserId = userId;
        Cash = cash;
        Issuers = issuers;

        BusinessErrors = new();
    }

    private Account(UserId userId, Cash cash, Issuers issuers,  BusinessErrors? businessErrors) 
    {
        UserId = userId;
        Cash = cash;
        Issuers = issuers;

        BusinessErrors = new();
        if(businessErrors is not null)
            BusinessErrors = businessErrors;
    }

    public void AddBusinessErrors(BusinessErrors businessErrors)
    {
        BusinessErrors = businessErrors;
    }

    public Account Clone(UserId? userId = null, Cash? cash = null, Issuers? issuers = null)
    {
        return new Account(
            userId ?? this.UserId,
            cash ?? this.Cash,
            issuers ?? this.Issuers,
            this.BusinessErrors
        );
    }

    public void UpdateIssuer(Issuer issuer)
    {
        Issuer? oldIssuer = this.Issuers
            .FirstOrDefault(issuer => issuer.IssuerName.Value.Equals(issuer.IssuerName.Value));

        if (oldIssuer is not null)
            this.Issuers.Remove(issuer);

        this.Issuers.Add(new Issuer(
            new(issuer.IssuerName.Value),
            new(issuer.TotalShares.Value),
            new(issuer.SharePrice.Value))
        );
    }
}
