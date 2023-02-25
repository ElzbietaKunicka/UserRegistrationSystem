using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using UserRegistrationSystem.Dto;
using UserRegistrationSystem.Migrations;

namespace UserRegistrationSystem.DAL
{
    public class PersonalInformationDbRepository : IPersonalInformationList
    {
        private readonly AccountsListDbContext _context;

        public PersonalInformationDbRepository(AccountsListDbContext context)
        {
            _context = context;
        }
        public static bool check(string str)
        {
            return (String.IsNullOrEmpty(str) ||
                  str.Trim().Length == 0) ? true : false;
        }
        public void AddNewPersonalInformation(int currentUserId,
            PersonalInformationDto personalInformationDto)
        {
            var userFromDb = _context.Accounts.FirstOrDefault(p => p.Id == currentUserId);

            if (userFromDb.PersonalInformationId == null)
            {
                if (false)
                {
                    //bool namecheck = check(personalInformationDto.Name);
                    bool codecheck = check(personalInformationDto.PersonalCode);
                }
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
            else
            {
                throw new Exception("Jau uzpildyta informacija, galite tik update");
            }
        }

        public IEnumerable<AccountDto> GetUsersIdAndUsernames()
        {
            var accounts = _context.Accounts.Select(account => new AccountDto
            { 
                Id= account.Id,
                UserName= account.UserName,
            });
            return accounts;
        }

        public int? getPersonalInformationIdByCurrentUser(int currentUserId)
        {
            var userFromDb = _context.Accounts.FirstOrDefault(p => p.Id == currentUserId);
            return userFromDb.PersonalInformationId;
        }
        public void UpdatePersonalInformation(int currentUserId,
            PersonalInformationDto personalInformationDto)
        {
            var userFromDb = _context.Accounts.Include(b => b.PersonalInformation)
                .ThenInclude(b => b.ResidentialAddress)
                .FirstOrDefault(p => p.Id == currentUserId);
            userFromDb.PersonalInformation.Name =
                personalInformationDto.Name.Trim();
            userFromDb.PersonalInformation.Surname =
                personalInformationDto.Surname.Trim();
            userFromDb.PersonalInformation.PersonalCode =
                personalInformationDto.PersonalCode.Trim();
            userFromDb.PersonalInformation.Phone =
                personalInformationDto.Phone.Trim();
            userFromDb.PersonalInformation.Email =
                personalInformationDto.Email.Trim();

            //if (userFromDb.PersonalInformation.ResidentialAddress == null)
            //{
            //    userFromDb.PersonalInformation.ResidentialAddress = new ResidentialAddress();
            //}
            userFromDb.PersonalInformation.ResidentialAddress.City =
                personalInformationDto.ResidentialAddress.City.Trim();
            userFromDb.PersonalInformation.ResidentialAddress.Street =
                personalInformationDto.ResidentialAddress.Street.Trim();
            userFromDb.PersonalInformation.ResidentialAddress.HomeNumber =
                personalInformationDto.ResidentialAddress.HomeNumber;
            userFromDb.PersonalInformation.ResidentialAddress.ApartmentNumber =
                personalInformationDto.ResidentialAddress.ApartmentNumber;
            _context.SaveChanges();
        }

        public PersonalInformation GetAllInfoAboutCurrentUser(int currentUserId)
        {
            var accountFromDb = _context.Accounts.Include(b => b.PersonalInformation).ThenInclude(b => b.ResidentialAddress)
                .FirstOrDefault(p => p.Id == currentUserId);
            return accountFromDb.PersonalInformation;
        }
        public string GetCurrentUserName(int currentUserId)
        {
            var accountFromDb = _context.Accounts
               .FirstOrDefault(p => p.Id == currentUserId);
            return accountFromDb.UserName;
        }

        public AccountDto getById(int accountId)
        {
            var accountFromDb = _context.Accounts
                .Include(a => a.PersonalInformation)
                .ThenInclude(p => p.ResidentialAddress)
                .FirstOrDefault(a => a.Id == accountId);

            if (accountFromDb == null)
            {
                return null;
            }

            var accountWithInfo = new AccountDto
            {
                UserName = accountFromDb.UserName,
                
            };
            if (accountFromDb.PersonalInformation != null)
            {
                accountWithInfo.PersonalInformation = new PersonalInformationDto
                {
                    Name = accountFromDb.PersonalInformation.Name,
                    Surname = accountFromDb.PersonalInformation.Surname,
                    PersonalCode = accountFromDb.PersonalInformation.PersonalCode,
                    Phone = accountFromDb.PersonalInformation.Phone,
                    Email = accountFromDb.PersonalInformation.Email
                };
            }
           
            if (accountFromDb.PersonalInformation?.ResidentialAddress != null)
            {
                accountWithInfo.PersonalInformation.ResidentialAddress = new ResidentialAddressDto
                {
                    City = accountFromDb.PersonalInformation.ResidentialAddress.City,
                    Street = accountFromDb.PersonalInformation.ResidentialAddress.Street,
                    HomeNumber = accountFromDb.PersonalInformation.ResidentialAddress.HomeNumber,
                    ApartmentNumber = accountFromDb.PersonalInformation.ResidentialAddress.ApartmentNumber
                };
            }
            return accountWithInfo;
        }

        public void DeleteAccountById(int id)
        {
            /// trinam kiekviena atskirai
            var acc = _context.Accounts.Include(p => p.PersonalInformation).ThenInclude(p => p.ResidentialAddress).FirstOrDefault(a => a.Id == id);
            _context.ResidentialAddresses.RemoveRange(acc.PersonalInformation.ResidentialAddress);
            _context.PersonalInformation.RemoveRange(acc.PersonalInformation);
            _context.Accounts.Remove(acc);
            _context.SaveChanges();
        }

        //public List<PersonalInformationDto> getPersonalInformationById(int personalInfoId)
        //{
        //    return _context.PersonalInformation.Where(x => x.Id == personalInfoId).Select(x => new PersonalInformationDto
        //    {
        //        Name = x.Name,
        //        Surname = x.Surname,
        //        PersonalCode = x.PersonalCode,
        //        Phone = x.Phone,
        //        Email = x.Email,
        //        ResidentialAddress = new ResidentialAddressDto
        //        {
        //            City = x.ResidentialAddress.City,
        //            Street = x.ResidentialAddress.Street,
        //            HomeNumber = x.ResidentialAddress.HomeNumber,
        //            ApartmentNumber = x.ResidentialAddress.ApartmentNumber,
        //        }
        //    }).ToList();
        //}

        //public IEnumerable<AccountDto> getAccountsInformationByName(string userName)
        //{
        //    return _context.Accounts.Where(x => x.UserName == userName).Select(x => new AccountDto
        //    {
        //        UserName = x.UserName,
        //        PersonalInformation = new PersonalInformationDto
        //        {
        //            Name = x.PersonalInformation.Name,
        //            Surname = x.PersonalInformation.Surname,
        //            PersonalCode = x.PersonalInformation.PersonalCode,
        //            Phone = x.PersonalInformation.Phone,
        //            Email = x.PersonalInformation.Email,
        //            ResidentialAddress = new ResidentialAddressDto
        //            {
        //                City = x.PersonalInformation.ResidentialAddress.City,
        //                Street = x.PersonalInformation.ResidentialAddress.Street,
        //                HomeNumber = x.PersonalInformation.ResidentialAddress.HomeNumber,
        //                ApartmentNumber = x.PersonalInformation.ResidentialAddress.ApartmentNumber,
        //            }
        //        }
        //    }).ToList();
        //}

        //public IEnumerable<string> GetUsersName()
        //{
        //    var accountsNamesList = _context.Accounts.Select(a => a.UserName);
        //    return accountsNamesList;
        //}




    }
}
         

    
       



