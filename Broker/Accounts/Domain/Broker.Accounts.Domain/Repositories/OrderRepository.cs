using Broker.Accounts.Domain.Entities.Write;

namespace Broker.Accounts.Domain.Repositories;

public interface IOrderRepository
{
    Task Create(WriteOrder order);
}
