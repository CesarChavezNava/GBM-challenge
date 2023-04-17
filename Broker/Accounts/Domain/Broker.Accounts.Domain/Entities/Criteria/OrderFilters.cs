using Broker.Accounts.Domain.ValueObjects;
using Broker.Core.Entities;
using Broker.Core.ValueObjects;

namespace Broker.Accounts.Domain.Entities.Criteria;

public class OrderFilters : IFilters
{
    public readonly UserId UserId;
    public readonly IssuerName? IssuerName;
    public readonly Operation? Operation;
    public readonly Timestamp? Timestamp;
    public readonly MinutesAgo? MinutesAgo;

    public OrderFilters(
        UserId userId,
        string? issuerName = null,
        string? operation = null,
        int? minutesAgo = null,
        long? timestamp = null)
    {
        UserId = userId;

        if(issuerName is not null)
            IssuerName = new(issuerName);

        if(operation is not null) 
            Operation = new(operation);

        if(timestamp is not null) 
            Timestamp = new((long)timestamp);

        if(minutesAgo is not null)
        {
            if(timestamp is null)
            {
                DateTime now = DateTime.Now; 
                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); 
                long milliseconds = (long)(now.ToLocalTime() - epoch).TotalMilliseconds;

                Timestamp = new(milliseconds);
            }

            MinutesAgo = new((int)minutesAgo);
        }
            
    }
}
