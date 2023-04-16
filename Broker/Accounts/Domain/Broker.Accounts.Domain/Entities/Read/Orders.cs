using System.Collections.ObjectModel;

namespace Broker.Accounts.Domain.Entities.Read;

public class Orders : Collection<Order>
{
    public Orders() : base() { }
    public Orders(IList<Order> orders) : base(orders) { }
}
