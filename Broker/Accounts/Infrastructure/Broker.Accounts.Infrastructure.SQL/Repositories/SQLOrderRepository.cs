using AutoMapper;
using Broker.Accounts.Domain.Entities.Criteria;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Accounts.Domain.Repositories;
using Broker.Accounts.Domain.ValueObjects;
using Broker.Accounts.Infrastructure.SQL.Schemas;
using Broker.Core.Entities;
using Broker.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

    public async Task<Orders> Search(Criteria<OrderFilters> criteria)
    {
        List<AccountOrderSchema> schemas = new();
        if(criteria.Limit is not null && criteria.Offset is not null) 
        {
            schemas = await context.Orders
                        .Where(Filter(criteria.Filters))
                        .Skip(criteria.Limit.Value)
                        .Take(criteria.Offset.Value)
                        .AsNoTracking()
                        .ToListAsync();
        } 
        else if (criteria.Limit is not null)
        {
            schemas = await context.Orders
                        .Where(Filter(criteria.Filters))
                        .Skip(criteria.Limit.Value)
                        .AsNoTracking()
                        .ToListAsync();
        }
        else if (criteria.Offset is not null)
        {
            schemas = await context.Orders
                        .Where(Filter(criteria.Filters))
                        .Take(criteria.Offset.Value)
                        .AsNoTracking()
                        .ToListAsync();
        } 
        else
        {
            schemas = await context.Orders
                        .Where(Filter(criteria.Filters))
                        .ToListAsync();
        }

        if (criteria.Order is null || criteria.Order.Value == SearchOrderCode.DESC)
            schemas = schemas.OrderBy(schema => schema.Timestamp).ToList();
        else
            schemas = schemas.OrderByDescending(schema => schema.Timestamp).ToList();


        return new Orders(
            schemas.Select(schema => new Order(
                new(schema.Timestamp),
                new(schema.Operation),
                new(schema.IssuerName),
                new(schema.TotalShares),
                new(schema.SharePrice)
            )).ToList());
    }

    private Expression<Func<AccountOrderSchema, bool>> Filter(OrderFilters filters) 
    {
        if (filters.MinutesAgo is not null && filters.Timestamp is not null)
        {
            return schema => 
                schema.UserId == filters.UserId.Value &&
                schema.IssuerName.Equals(filters.IssuerName == null ?
                    schema.IssuerName :
                    filters.IssuerName.Value) &&
                schema.Operation == (filters.Operation == null ?
                    schema.Operation :
                    filters.Operation.Value) &&
                schema.Timestamp >= filters.MinutesAgo.Subtraction(filters.Timestamp.Value);
        }
        else
        {
            return schema =>
                schema.UserId == filters.UserId.Value &&
                schema.IssuerName.Equals(filters.IssuerName == null ?
                    schema.IssuerName :
                    filters.IssuerName.Value) &&
                schema.Operation == (filters.Operation == null ?
                    schema.Operation :
                    filters.Operation.Value);
        }
    }
}
