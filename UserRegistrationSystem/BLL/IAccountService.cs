namespace ZmogausUzregistravimoSistema.BLL
{
    public interface IAccountService
    {
        Account SignupNewAccount(string username, string password);

        (bool, Account) Login(string username, string password);
    }
}
