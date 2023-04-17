namespace Broker.Accounts.Infrastructure.API.Dtos;

public class OperationDto
{
    public long Timestamp { get; set; }
    public string Operation { get; set; } = null!;
    public string IssuerName { get; set; } = null!;
    public int TotalShares { get; set; }
    public decimal SharePrice { get; set; }
}
