using AutoMapper;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Repositories;
using Broker.Accounts.Infrastructure.SQL.Schemas;

namespace Broker.Accounts.Infrastructure.SQL.Repositories;

public class SQLOrderRepository : IOrderRepository
{
    private readonly AccountContext context;
    private readonly IMapper mapper;

    public SQLOrderRepository(AccountContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task Create(WriteOrder order)
    {
        AccountOrderSchema schema = mapper.Map<AccountOrderSchema>(order);
        context.Orders.Add(schema);
        await context.SaveChangesAsync();
    }
}
