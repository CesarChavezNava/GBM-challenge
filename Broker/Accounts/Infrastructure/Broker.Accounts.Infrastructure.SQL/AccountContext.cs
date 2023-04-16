using Broker.Accounts.Infrastructure.SQL.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Broker.Accounts.Infrastructure.SQL;

public class AccountContext : DbContext
{
    public AccountContext(DbContextOptions<AccountContext> options)
        : base(options)
    { }

    public DbSet<AccountSchema> Accounts { get; set; }
    public DbSet<AccountIssuerSchema> Issuers { get; set; }
    public DbSet<AccountOrderSchema> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AccountIssuerSchema>().HasKey(table => new
        {
            table.UserId,
            table.IssuerName
        });

        modelBuilder.Entity<AccountOrderSchema>().HasKey(table => new
        {
            table.UserId,
            table.OrderId
        });
    }
}
