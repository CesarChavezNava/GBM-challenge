using Broker.Accounts.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Broker.Accounts.Infrastructure.SQL.Schemas;

[Table("ACCOUNT_ORDER")]
public class AccountOrderSchema
{
    [Key]
    [Column("User_Id", Order = 1)]
    public int UserId { get; set; }

    [Key]
    [Column("Order_Id", Order = 2)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }

    [Column("Date")]
    public long Timestamp { get; set; }

    [Column("Operation")]
    [MaxLength(15)]
    [EnumDataType(typeof(OperationCode))]
    public OperationCode Operation { get; set; }

    [Column("Issuer_Name")]
    [MaxLength(10)]
    public string IssuerName { get; set; } = null!;

    [Column("Total_Shares")]
    public int TotalShares { get; set; }

    [Column("Share_Price", TypeName = "decimal(12,2)")]
    public decimal SharePrice { get; set; }

    [ForeignKey("UserId")]
    public virtual AccountSchema Account { get; set; } = null!;
}
