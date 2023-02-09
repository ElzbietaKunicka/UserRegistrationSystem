using Microsoft.EntityFrameworkCore;
using UserRegistrationSystem.Dto;

namespace UserRegistrationSystem.DAL
{
    public class PersonalInformationDbRepository : IPersonalInformationList
    {
        private readonly AccountsListDbContext _context;
        private int _lastId;
        public PersonalInformationDbRepository(AccountsListDbContext context)
        {
            _context = context;
        }

        public void AddNewPersonalInformation(int currentUserId, PersonalInformationDto personalInformationDto)
        {
            //var userFromDb = _context.Accounts.Include(b => b.PersonalInformation).ThenInclude(b => b.ResidentialAddress).FirstOrDefault(p => p.Id== currentUserId);
            var userFromDb = _context.Accounts.FirstOrDefault(p => p.Id == currentUserId);
            userFromDb.PersonalInformation = new PersonalInformation
            {
                Name = personalInformationDto.Name,
                Surname = personalInformationDto.Surname,
                PersonalCode = personalInformationDto.PersonalCode,
                Phone = personalInformationDto.Phone,
                Email = personalInformationDto.Email,
                ResidentialAddress = new ResidentialAddress
                {
                    City = personalInformationDto.ResidentialAddress.City,
                    Street = personalInformationDto.ResidentialAddress.Street,
                    HomeNumber = personalInformationDto.ResidentialAddress.HomeNumber,
                    ApartmentNumber = personalInformationDto.ResidentialAddress.ApartmentNumber,

                },
            };
            _context.SaveChanges();
        }

        public PersonalInformation getPersonalInformationById(int currentUserId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePersonalInformation(int currentUserId, string name, string surname, int personalCode, string phone, string email, ResidentialAddress residentialAddress)
        {
            throw new NotImplementedException();
        }
    }
}
