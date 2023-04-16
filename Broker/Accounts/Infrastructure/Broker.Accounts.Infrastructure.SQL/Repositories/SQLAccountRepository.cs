using AutoMapper;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Exceptions;
using Broker.Accounts.Domain.Repositories;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Accounts.Infrastructure.SQL.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Broker.Accounts.Infrastructure.SQL.Repositories;

public class SQLAccountRepository : IAccountRepository
{
    private readonly AccountContext context;
    private readonly IMapper mapper;

    public SQLAccountRepository(AccountContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<Account> Create(WriteAccount account)
    {
        AccountSchema schema = mapper.Map<AccountSchema>(account);
        context.Accounts.Add(schema);
        await context.SaveChangesAsync();

        return new Account(new(schema.UserId), new(account.Cash.Value));
    }

    public async Task<Account> Find(UserId userId)
    {
        AccountSchema? accountSchema = await context.Accounts
            .Include(account => account.Issuers)
            .AsNoTracking()
            .FirstOrDefaultAsync(account => account.UserId == userId.Value);

        if (accountSchema is null)
            throw new UserNotFoundException();

        Issuers issuers = new();
        if (accountSchema.Issuers is null)
            return new Account(new(userId.Value), new(accountSchema.Balance), issuers);

        if(accountSchema.Issuers.Count > 0)
        {
            issuers = new(
                    accountSchema.Issuers.Select(issuer =>
                        new Issuer(
                            new IssuerName(issuer.IssuerName),
                            new TotalShares(issuer.TotalShares),
                            new SharePrice(issuer.SharePrice)
                        )
                    ).ToList()
                );
        }

        return new Account(new(userId.Value), new(accountSchema.Balance), issuers);
    }

    public async Task<Account> Update(WriteAccount account)
    {
        AccountSchema schema = mapper.Map<AccountSchema>(account);
        context.Accounts.Update(schema);
        await context.SaveChangesAsync();

        return new Account(new(account.UserId.Value), new(account.Cash.Value));
    }
}
