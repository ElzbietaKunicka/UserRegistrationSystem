namespace UserRegistrationSystem.DAL
{
    public interface IAccountRepository
    {
        void SaveAccount(Account account);
        Account GetAccount(string username);
    }
}
