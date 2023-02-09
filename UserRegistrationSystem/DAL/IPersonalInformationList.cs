using UserRegistrationSystem.Dto;

namespace UserRegistrationSystem.DAL
{
    public interface IPersonalInformationList
    {
        void UpdatePersonalInformation(int currentUserId, PersonalInformationDto personalInformationDto);
        PersonalInformation getPersonalInformationById(int currentUserId);
        void AddNewPersonalInformation(int currentUserId, PersonalInformationDto personalInformationDto);
    }
}
