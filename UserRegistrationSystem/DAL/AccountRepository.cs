using UserRegistrationSystem.Models;

namespace UserRegistrationSystem.DAL
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountsListDbContext _context;
        public AccountRepository(AccountsListDbContext context)
        {
            _context = context;
        }
        public Account GetAccount(string username)
        {
            return _context.Accounts.FirstOrDefault(a => a.UserName == username);
        }
        public void SaveAccount(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }
    }
}
