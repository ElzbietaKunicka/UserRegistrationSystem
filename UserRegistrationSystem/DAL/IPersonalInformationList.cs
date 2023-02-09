using UserRegistrationSystem.Dto;

namespace UserRegistrationSystem.DAL
{
    public interface IPersonalInformationList
    {
        void UpdatePersonalInformation(string name, string surname, int personalCode, string phone, string email, ResidentialAddress residentialAddress);
        PersonalInformation getPersonalInformationById(int id);
        void AddNewPersonalInformation(PersonalInformationDto personalInformationDto);
        
    }
}
