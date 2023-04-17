using Broker.Accounts.Domain.Entities.Criteria;
using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Repositories;
using Broker.Core.Entities;

namespace Broker.Accounts.Application.Search;

public class OrdersSearcher : IForSearchOrders
{
    private readonly IOrderRepository orderRepository;
    public OrdersSearcher(IOrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public async Task<Orders> Search(Criteria<OrderFilters> criteria)
    {
        return await orderRepository.Search(criteria);
    }
}
