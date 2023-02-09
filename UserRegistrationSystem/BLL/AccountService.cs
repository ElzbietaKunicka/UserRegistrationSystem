using System.Security.Cryptography;
using System.Text;
using UserRegistrationSystem.DAL;
using UserRegistrationSystem.Dto;

namespace UserRegistrationSystem.BLL
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public (bool, Account) Login(string username, string password)
        {
            var account = _accountRepository.GetAccount(username);
            if (account == null)
            {
                return (false, null);
            }
            if (VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
            {
                return (true, account);
            }
            else
            {
                return (false, null);
            }
        }

        public Account SignupNewAccount(string username, string password)
        {
            var account = CreateAccount(username, password);
            _accountRepository.SaveAccount(account);
            return account;
        }
        private Account CreateAccount(string username, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            var account = new Account
            {
                UserName = username,
                Password = password,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "User"
            };
            return account;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        private bool VerifyPasswordHash(string password, byte[] passwordhash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordhash);
        }
    }
}
