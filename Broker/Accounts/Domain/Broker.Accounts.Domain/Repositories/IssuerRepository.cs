using Broker.Accounts.Domain.Entities.Write;

namespace Broker.Accounts.Domain.Repositories;

public interface IIssuerRepository
{
    Task Create(WriteIssuer issuer);
}
