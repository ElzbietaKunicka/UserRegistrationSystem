using UserRegistrationSystem.Dto;

namespace UserRegistrationSystem.DAL
{
    public interface IPersonalInformationList
    {
        void UpdatePersonalInformation(int currentUserId, PersonalInformationDto personalInformationDto);
        //PersonalInformation getPersonalInformationById(int accountId);
        List<PersonalInformationDto> getPersonalInformationById(int id);
        void AddNewPersonalInformation(int currentUserId, PersonalInformationDto personalInformationDto);

        int? getPersonalInformationIdByCurrentUser(int currentUserId);
        //List<Account> GetAllUsersAccount(AccountDto accDto);

        IEnumerable<AccountDto> GetAllInfoAboutUsers();
        IEnumerable<string> GetUsersName();

        PersonalInformation GetAllInfoAboutCurrentUser(int currentUserId);
    }
}
