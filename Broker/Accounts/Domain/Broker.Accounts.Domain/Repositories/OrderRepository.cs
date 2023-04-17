using Broker.Accounts.Domain.Entities.Criteria;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;
using Broker.Core.Entities;

namespace Broker.Accounts.Domain.Repositories;

public interface IOrderRepository
{
    Task Create(WriteOrder order);
    Task<Orders> Search(Criteria<OrderFilters> criteria);
}
