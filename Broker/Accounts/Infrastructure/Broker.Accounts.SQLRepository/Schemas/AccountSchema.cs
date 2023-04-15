using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Broker.Accounts.SQLRepository.Schemas;


[Table("ACCOUNT")]
public class AccountSchema
{
    [Key]
    [Column("User_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [Column("Balance", TypeName = "decimal(12, 2)")]
    public decimal Balance { get; set; }
}
