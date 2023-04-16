using Broker.Accounts.Domain.Entities.Read;
using Broker.Accounts.Domain.Entities.Write;

namespace Broker.Accounts.Application.Create.Helpers;

public class Transformer
{
    private Transformer() { }

    public static Issuer FromWriteIssuer(WriteIssuer writeIssuer)
    {
        return new(
            writeIssuer.IssuerName,
            writeIssuer.TotalShares,
            writeIssuer.SharePrice
        );
    }
}
