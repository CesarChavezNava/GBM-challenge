using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Broker.Accounts.Infrastructure.SQL.Schemas;

[Table("ACCOUNT_ISSUER")]
public class AccountIssuerSchema
{
    [Key]
    [Column("User_Id", Order = 1)]
    public int UserId { get; set; }

    [Key]
    [Column("Issuer_Name", Order = 2)]
    [MaxLength(10)]
    public string IssuerName { get; set; } = null!;

    [Column("Total_Shares")]
    public int TotalShares { get; set; }

    [Column("Share_Price", TypeName = "decimal(12, 2)")]
    public decimal SharePrice { get; set; }

    [ForeignKey("UserId")]
    public virtual AccountSchema Account { get; set; } = null!;
}
