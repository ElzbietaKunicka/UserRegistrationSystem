using Microsoft.EntityFrameworkCore;

namespace UserRegistrationSystem.Models;

public class AccountsListDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<PersonalInformation> PersonalInformation { get; set; }
    public DbSet<ResidentialAddress> ResidentialAddresses { get; set; }
    public AccountsListDbContext(DbContextOptions<AccountsListDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Account>()
            .HasIndex(a => a.UserName)
            .IsUnique();
    }
}



