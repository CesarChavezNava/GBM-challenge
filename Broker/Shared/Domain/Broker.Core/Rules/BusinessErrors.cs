using System.Collections.ObjectModel;

namespace Broker.Core.Rules;

public class BusinessErrors : Collection<string>
{
    public bool HasErrors()
    {
        return this.Count > 0;
    }
}
