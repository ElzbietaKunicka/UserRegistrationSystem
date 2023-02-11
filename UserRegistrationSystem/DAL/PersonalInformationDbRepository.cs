using System.Numerics;
using Microsoft.AspNetCore.Mvc;
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
                Name = personalInformationDto.Name.Trim(),
                Surname = personalInformationDto.Surname.Trim(),
                PersonalCode = personalInformationDto.PersonalCode.Trim(),
                Phone = personalInformationDto.Phone.Trim(),
                Email = personalInformationDto.Email.Trim(),
                ResidentialAddress = new ResidentialAddress
                {
                    City = personalInformationDto.ResidentialAddress.City.Trim(),
                    Street = personalInformationDto.ResidentialAddress.Street.Trim(),
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
            //var userFromDb = _context.Accounts.Include(b => b.PersonalInformation)
            //    .ThenInclude(b => b.ResidentialAddress)
            //    .FirstOrDefault(p => p.Id == currentUserId);
            var userFromDb = _context.Accounts.FirstOrDefault(p => p.Id == currentUserId);
            userFromDb.PersonalInformation = new PersonalInformation
            {
                Name = personalInformationDto.Name.Trim(),
                Surname = personalInformationDto.Surname.Trim(),
                PersonalCode = personalInformationDto.PersonalCode.Trim(),
                Phone = personalInformationDto.Phone.Trim(),
                Email = personalInformationDto.Email.Trim(),
                ResidentialAddress = new ResidentialAddress
                {
                    City = personalInformationDto.ResidentialAddress.City.Trim(),
                    Street = personalInformationDto.ResidentialAddress.Street.Trim(),
                    HomeNumber = personalInformationDto.ResidentialAddress.HomeNumber,
                    ApartmentNumber = personalInformationDto.ResidentialAddress.ApartmentNumber,
                },
            };

            //string[] values = 
            //    { 
            //    personalInformationDto.Name, 
            //    personalInformationDto.Surname, 
            //    personalInformationDto.PersonalCode,
            //    personalInformationDto.Phone, 
            //    personalInformationDto.Email, 
            //    personalInformationDto.ResidentialAddress.City,
            //    personalInformationDto.ResidentialAddress.Street, 
            //    $"{personalInformationDto.ResidentialAddress.HomeNumber}", 
            //    $"{personalInformationDto.ResidentialAddress.ApartmentNumber}"
            //};

            //foreach (var value in values)
            //{

            //    if (String.IsNullOrWhiteSpace(value) == false)
            //    {
            //        userFromDb.PersonalInformation.Name = 
            //            personalInformationDto.Name.Trim();
            //        userFromDb.PersonalInformation.Surname = 
            //            personalInformationDto.Surname.Trim();
            //        userFromDb.PersonalInformation.PersonalCode = 
            //            personalInformationDto.PersonalCode.Trim();
            //        userFromDb.PersonalInformation.Phone = 
            //            personalInformationDto.Phone.Trim();
            //        userFromDb.PersonalInformation.Email = 
            //            personalInformationDto.Email.Trim();
            //        userFromDb.PersonalInformation.ResidentialAddress.City =
            //            personalInformationDto.ResidentialAddress.City.Trim();
            //        userFromDb.PersonalInformation.ResidentialAddress.Street =
            //            personalInformationDto.ResidentialAddress.Street.Trim();
            //        userFromDb.PersonalInformation.ResidentialAddress.HomeNumber =
            //            personalInformationDto.ResidentialAddress.HomeNumber;
            //        userFromDb.PersonalInformation.ResidentialAddress.ApartmentNumber =
            //            personalInformationDto.ResidentialAddress.ApartmentNumber;
            //    }
            //    else
            //    {
            //        throw new ArgumentException("Username cannot be empty");
            //    }
            //}
            _context.SaveChanges();
        }
    }
}
