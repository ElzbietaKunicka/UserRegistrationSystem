using UserRegistrationSystem.Dto;

namespace UserRegistrationSystem.DAL
{
    public class PersonalInformationDbRepository : IPersonalInformationList
    {
        public void AddNewPersonalInformation(PersonalInformationDto personalInformationDto)
        {
            throw new NotImplementedException();
        }

        public PersonalInformation getPersonalInformationById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePersonalInformation(string name, string surname, int personalCode, string phone, string email, ResidentialAddress residentialAddress)
        {
            throw new NotImplementedException();
        }
    }
}
