using AutoMapper;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Repositories;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Accounts.Infrastructure.SQL.Schemas;

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

        return new Account(new UserId(schema.UserId), account.Cash);
    }
}
