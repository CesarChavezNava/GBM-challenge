using Broker.Accounts.Infrastructure.SQL.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Broker.Accounts.Infrastructure.SQL;

public class AccountContext : DbContext
{
    public AccountContext(DbContextOptions<AccountContext> options)
        : base(options)
    { }

    public DbSet<AccountSchema> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
