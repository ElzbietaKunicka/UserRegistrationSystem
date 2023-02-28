using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using UserRegistrationSystem.Migrations;
using UserRegistrationSystem.Models;

namespace UserRegistrationSystem.DAL
{
    public class PersonalInformationDbRepository : IPersonalInformationList
    {
        private readonly AccountsListDbContext _context;

        public PersonalInformationDbRepository(AccountsListDbContext context)
        {
            _context = context;
        }
        public void AddNewPersonalInformation(int currentUserId,
            PersonalInformationDto personalInformationDto)
        {
            var userFromDb = _context.Accounts
                .FirstOrDefault(p => p.Id == currentUserId);

            if (userFromDb.PersonalInformationId == null)
            {
                userFromDb.PersonalInformation = new PersonalInformation
                {
                    Name = personalInformationDto.Name.Trim(),
                    Surname = personalInformationDto.Surname.Trim(),
                    PersonalCode = personalInformationDto
                    .PersonalCode.Trim(),
                    Phone = personalInformationDto.Phone.Trim(),
                    Email = personalInformationDto.Email.Trim(),
                    ResidentialAddress = new ResidentialAddress
                    {
                        City = personalInformationDto
                        .ResidentialAddress.City.Trim(),
                        Street = personalInformationDto
                        .ResidentialAddress.Street.Trim(),
                        HomeNumber = personalInformationDto
                        .ResidentialAddress.HomeNumber,
                        ApartmentNumber = personalInformationDto
                        .ResidentialAddress.ApartmentNumber,
                    },
                };
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("The information was filled, now you can only update");
            }
        }
        public IEnumerable<AccountDto> GetUsersIdAndUsernames()
        {
            var accounts = _context.Accounts.Select(account =>
            new AccountDto
            { 
                Id= account.Id,
                UserName= account.UserName,
            });
            return accounts;
        }
        public int? getPersonalInformationIdByCurrentUser(int currentUserId)
        {
            var userFromDb = _context.Accounts
                .FirstOrDefault(p => p.Id == currentUserId);
            return userFromDb.PersonalInformationId;
        }
        public void UpdatePersonalInformation(int currentUserId,
            PersonalInformationDto personalInformationDto)
        {
            var userFromDb = _context.Accounts
                .Include(b => b.PersonalInformation)
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
            var accountFromDb = _context.Accounts
                .Include(b => b.PersonalInformation)
                .ThenInclude(b => b.ResidentialAddress)
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
                Id = accountFromDb.Id,
                UserName = accountFromDb.UserName,
            };
            if (accountFromDb.PersonalInformation != null)
            {
                accountWithInfo.PersonalInformation = new
                    PersonalInformationDto
                {
                    Name = accountFromDb.PersonalInformation.Name,
                    Surname = accountFromDb.PersonalInformation.Surname,
                    PersonalCode = accountFromDb.PersonalInformation.PersonalCode,
                    Phone = accountFromDb.PersonalInformation.Phone,
                    Email = accountFromDb.PersonalInformation.Email,
                    ResidentialAddress = new
                    ResidentialAddressDto
                    {
                        City = accountFromDb.PersonalInformation.
                    ResidentialAddress.City,
                        Street = accountFromDb.PersonalInformation.
                    ResidentialAddress.Street,
                        HomeNumber = accountFromDb.PersonalInformation.
                    ResidentialAddress.HomeNumber,
                        ApartmentNumber = accountFromDb.PersonalInformation.
                    ResidentialAddress.ApartmentNumber
                    }
                };
            }
            return accountWithInfo;
        }

        public void DeleteAccountById(int id)
        {
            var accountFromDb = _context.Accounts
                .Include(p => p.PersonalInformation)
                .ThenInclude(p => p.ResidentialAddress)
                .FirstOrDefault(a => a.Id == id);
            var accountFromDbWithoutInfo = _context.Accounts
                .FirstOrDefault(p => p.Id == id);
            if (accountFromDb == null)
            {
               return;
            }
            if (accountFromDbWithoutInfo.PersonalInformationId == null)
            {
                _context.Accounts.Remove(accountFromDbWithoutInfo);
                _context.SaveChanges();
                return;
            }
            _context.ResidentialAddresses
                .RemoveRange(accountFromDb.PersonalInformation
                .ResidentialAddress);
            _context.PersonalInformation
                .RemoveRange(accountFromDb.PersonalInformation);
            _context.Accounts.Remove(accountFromDb);
            _context.SaveChanges();
        }
    }
}
         

    
       



