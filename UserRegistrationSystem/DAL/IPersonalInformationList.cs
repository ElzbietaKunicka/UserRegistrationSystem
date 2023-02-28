using Microsoft.AspNetCore.Mvc;
using UserRegistrationSystem.Models;

namespace UserRegistrationSystem.DAL
{
    public interface IPersonalInformationList
    {
        void UpdatePersonalInformation(int currentUserId, 
            PersonalInformationDto personalInformationDto);
        void AddNewPersonalInformation(int currentUserId, 
            PersonalInformationDto personalInformationDto);
        int? getPersonalInformationIdByCurrentUser(int currentUserId);
        PersonalInformation GetAllInfoAboutCurrentUser(int currentUserId);
        string GetCurrentUserName(int currentUserId);
        AccountDto getById(int accountId);
        void DeleteAccountById(int id);
        IEnumerable<AccountDto> GetUsersIdAndUsernames();
    }
}
