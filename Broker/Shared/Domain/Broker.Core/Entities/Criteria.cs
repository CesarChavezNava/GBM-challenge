using Broker.Core.Enums;
using Broker.Core.ValueObjects;

namespace Broker.Core.Entities;

public class Criteria<T> where T : IFilters
{
    public readonly T Filters;
    public readonly SearchOrder? Order;
    public readonly SearchLimit? Limit;
    public readonly SearchOffset? Offset;

    public Criteria(T filters, SearchOrder? order = null, SearchLimit? limit = null, SearchOffset? offset = null)
    {
        Filters = filters;
        Order = new(SearchOrderCode.DESC);

        if (order is not null)
            Order = order;

        if(limit is not null) 
            Limit = limit;
        
        if (offset is not null) 
            Offset = offset;
    }
}
