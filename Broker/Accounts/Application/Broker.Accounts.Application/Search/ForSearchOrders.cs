using Broker.Accounts.Domain.Entities.Criteria;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Core.Entities;

namespace Broker.Accounts.Application.Search;

public interface IForSearchOrders
{
    Task<Orders> Search(Criteria<OrderFilters> criteria);
}
