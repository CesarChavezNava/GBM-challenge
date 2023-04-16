using System.Collections.ObjectModel;

namespace Broker.Accounts.Domain.Entities.Read;

public class Issuers : Collection<Issuer>
{
    public Issuers() : base() { }
    public Issuers(IList<Issuer> issuers) : base(issuers) { }
}
