namespace Broker.Accounts.Infrastructure.API.Dtos;

public class IssuerDto
{
    public string IssuerName { get; set; } = null!;
    public int TotalShares { get; set; }
    public decimal SharePrice { get; set; }
}
