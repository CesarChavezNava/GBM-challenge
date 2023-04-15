using Broker.Accounts.Domain.Entities.Write;
using Broker.Core.Rules;

namespace Broker.Accounts.Domain.Rules;

/// <summary>
/// All operations must happen between 6am and 3pm.
/// </summary>
public class ClosedMarketRule : BusinessRule<WriteOrder>
{
    public ClosedMarketRule()
        : base("CLOSED_MARKET")
    { }

    protected override bool Validate(WriteOrder order)
    {
        DateTime epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime date = epoch.AddMilliseconds(order.Timestamp.Value).ToLocalTime();

        return date.Hour >= 6 && date.Hour <= 15;
    }
}
