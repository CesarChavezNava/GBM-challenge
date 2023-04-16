namespace Broker.Accounts.Infrastructure.API.Dtos;

public class BalanceDto
{
    public decimal Cash { get; set; }
    public IssuerDto[] Issuers { get; set; } = null!;
}
