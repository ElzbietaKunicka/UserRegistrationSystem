using UserRegistrationSystem.Dto;

namespace UserRegistrationSystem.DAL
{
    public interface IPersonalInformationList
    {
        void UpdatePersonalInformation(int currentUserId, string name, string surname, int personalCode, string phone, string email, ResidentialAddress residentialAddress);
        PersonalInformation getPersonalInformationById(int currentUserId);
        void AddNewPersonalInformation(int currentUserId, PersonalInformationDto personalInformationDto);
        
    }
}
