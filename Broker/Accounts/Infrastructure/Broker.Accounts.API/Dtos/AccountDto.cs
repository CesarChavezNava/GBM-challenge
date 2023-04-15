namespace Broker.Accounts.API.Dtos;

public class AccountDto
{
    public int Id { get; set; }
    public decimal Cash { get; set; }
    public IssuerDto[]? Issuers { get; set; }
}
