﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Broker.Accounts.Infrastructure.SQL.Schemas;


[Table("ACCOUNT")]
public class AccountSchema
{
    [Key]
    [Column("User_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [Column("Balance", TypeName = "decimal(12, 2)")]
    public decimal Balance { get; set; }

    public virtual ICollection<AccountIssuerSchema>? Issuers { get; set; }
    public virtual ICollection<AccountOrderSchema>? Orders { get; set; }
}
