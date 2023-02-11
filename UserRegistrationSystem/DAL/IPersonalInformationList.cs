using UserRegistrationSystem.Dto;

namespace UserRegistrationSystem.DAL
{
    public interface IPersonalInformationList
    {
        void UpdatePersonalInformation(int currentUserId, PersonalInformationDto personalInformationDto);
        //PersonalInformation getPersonalInformationById(int accountId);
        List<PersonalInformationDto> getPersonalInformationById(int id);
        void AddNewPersonalInformation(int currentUserId, PersonalInformationDto personalInformationDto);
    }
}
