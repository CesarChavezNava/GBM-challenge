using Broker.Accounts.SQLRepository.Schemas;
using Microsoft.EntityFrameworkCore;

namespace Broker.Accounts.SQLRepository;

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
