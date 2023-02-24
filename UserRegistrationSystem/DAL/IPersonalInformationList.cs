using UserRegistrationSystem.Dto;

namespace UserRegistrationSystem.DAL
{
    public interface IPersonalInformationList
    {
        void UpdatePersonalInformation(int currentUserId, PersonalInformationDto personalInformationDto);
        void AddNewPersonalInformation(int currentUserId, PersonalInformationDto personalInformationDto);
        int? getPersonalInformationIdByCurrentUser(int currentUserId);
        IEnumerable<string> GetUsersName();
        PersonalInformation GetAllInfoAboutCurrentUser(int currentUserId);
        string GetCurrentUserName(int currentUserId);
        AccountDto getById(int accountId);
        //List<Account> GetAllUsersAccount(AccountDto accDto);
        //IEnumerable<AccountDto> GetAllInfoAboutUsers();
        //IEnumerable<AccountDto> getAccountsInformationByName(string userName);
        //PersonalInformation getPersonalInformationById(int accountId);
        //List<PersonalInformationDto> getPersonalInformationById(int id);
        IEnumerable<AccountDto> GetUsersIdAndUsernames();
    }
}
