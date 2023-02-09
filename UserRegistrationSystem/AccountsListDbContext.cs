using Microsoft.EntityFrameworkCore;

namespace UserRegistrationSystem;

public class AccountsListDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<PersonalInformation> PersonalInformation { get; set; }
    public DbSet<ResidentialAddress> ResidentialAddresses { get; set; }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=AccountsListDb;Trusted_Connection=True;");
    //}
    public AccountsListDbContext(DbContextOptions<AccountsListDbContext> options) : base(options)
    {

    }
}



