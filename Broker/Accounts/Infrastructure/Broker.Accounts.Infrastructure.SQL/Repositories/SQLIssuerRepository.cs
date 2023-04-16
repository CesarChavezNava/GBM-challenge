using AutoMapper;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Repositories;
using Broker.Accounts.Infrastructure.SQL.Schemas;

namespace Broker.Accounts.Infrastructure.SQL.Repositories;

public class SQLIssuerRepository : IIssuerRepository
{
    private readonly AccountContext context;
    private readonly IMapper mapper;

    public SQLIssuerRepository(AccountContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task Create(WriteIssuer issuer)
    {
        AccountIssuerSchema schema = mapper.Map<AccountIssuerSchema>(issuer);
        context.Issuers.Add(schema);
        await context.SaveChangesAsync();
    }

    public async Task Update(WriteIssuer issuer)
    {
        AccountIssuerSchema schema = mapper.Map<AccountIssuerSchema>(issuer);
        context.Issuers.Update(schema);
        await context.SaveChangesAsync();
    }
}
