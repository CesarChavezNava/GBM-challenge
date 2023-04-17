namespace Broker.Accounts.Infrastructure.API.Dtos;

public class SearchOrdersDto
{
    public string? IssuerName { get; set; } = null;
    public string? Opeartion { get; set; } = null;
    public string? Order { get; set; } = null;
    public int? Limit { get; set; } = null;
    public int? Offset { get; set; } = null;
}
