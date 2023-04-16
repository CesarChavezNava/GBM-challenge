namespace Broker.Accounts.Infrastructure.API.Dtos;

public class OrderDto
{
    public BalanceDto CurrentBalance { get; set; } = null!;
    public string[]? BusinessErrors { get; set; }
}
