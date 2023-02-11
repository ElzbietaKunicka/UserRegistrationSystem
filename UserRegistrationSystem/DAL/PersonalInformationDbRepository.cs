using System.Numerics;
using Microsoft.EntityFrameworkCore;
using UserRegistrationSystem.Dto;

namespace UserRegistrationSystem.DAL
{
    public class PersonalInformationDbRepository : IPersonalInformationList
    {
        private readonly AccountsListDbContext _context;
        
       
        //private int _lastId;
        public PersonalInformationDbRepository(AccountsListDbContext context)
        {
            _context = context;
        }
        public void AddNewPersonalInformation(int currentUserId, 
            PersonalInformationDto personalInformationDto)
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
        //public PersonalInformation getPersonalInformationById(int accountId)
        //{
        //    var account = _context.Accounts.Include(b => b.PersonalInformation).ThenInclude(b => b.ResidentialAddress)
        //        .FirstOrDefault(p => p.Id == accountId);
        //    return account.PersonalInformation;
        //}
        public List<PersonalInformationDto> getPersonalInformationById(int personalInfoId)
        {
            return _context.PersonalInformation.Where(x => x.Id == personalInfoId).Select(x => new PersonalInformationDto
            {
                Name = x.Name,
                Surname = x.Surname,
                PersonalCode = x.PersonalCode,
                Phone = x.Phone,
                Email = x.Email,
                ResidentialAddress = new ResidentialAddressDto
                {
                    City = x.ResidentialAddress.City,
                    Street = x.ResidentialAddress.Street,
                    HomeNumber = x.ResidentialAddress.HomeNumber,
                    ApartmentNumber = x.ResidentialAddress.ApartmentNumber,
                }
            }).ToList();
        }


        public void UpdatePersonalInformation(int currentUserId,
            PersonalInformationDto personalInformationDto)
        {
            var userFromDb = _context.Accounts.Include(b => b.PersonalInformation)
                .ThenInclude(b => b.ResidentialAddress).FirstOrDefault(p => p.Id == currentUserId);
            userFromDb.PersonalInformation.Name = personalInformationDto.Name;
            userFromDb.PersonalInformation.Surname = personalInformationDto.Surname;
            userFromDb.PersonalInformation.PersonalCode = personalInformationDto.PersonalCode;
            userFromDb.PersonalInformation.Phone = personalInformationDto.Phone;
            userFromDb.PersonalInformation.Email = personalInformationDto.Email;

            userFromDb.PersonalInformation.ResidentialAddress.City = 
                personalInformationDto.ResidentialAddress.City;
            userFromDb.PersonalInformation.ResidentialAddress.Street = 
                personalInformationDto.ResidentialAddress.Street;
            userFromDb.PersonalInformation.ResidentialAddress.HomeNumber = 
                personalInformationDto.ResidentialAddress.HomeNumber;
            userFromDb.PersonalInformation.ResidentialAddress.ApartmentNumber = 
                personalInformationDto.ResidentialAddress.ApartmentNumber;
            _context.SaveChanges();
        }
    }
}
