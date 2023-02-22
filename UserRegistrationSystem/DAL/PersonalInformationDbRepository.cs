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

            if(userFromDb.PersonalInformationId == null)
            {
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
        
        public IEnumerable<AccountDto> GetAllInfoAboutUsers()
        {
            var accounts = _context.Accounts.Include(b => b.PersonalInformation).ThenInclude(b => b.ResidentialAddress).ToList();
            List<AccountDto> accountDtosList = new List<AccountDto>();
            foreach (var acc in accounts)
            {
                if (acc.PersonalInformation != null)
                {
                    //var humanInfo = acc.PersonalInformation;
                    if (acc.PersonalInformation.ResidentialAddress != null)
                    {
                       // var address = acc.PersonalInformation.ResidentialAddress;
                        var accountDto = new AccountDto
                        {
                            UserName= acc.UserName,
                            PersonalInformation = new PersonalInformationDto
                            {
                                Name = acc.PersonalInformation.Name,
                                Surname = acc.PersonalInformation.Surname,
                                PersonalCode = acc.PersonalInformation.PersonalCode,
                                Phone = acc.PersonalInformation.Phone,
                                Email = acc.PersonalInformation.Email,
                                ResidentialAddress = new ResidentialAddressDto
                                {
                                    City = acc.PersonalInformation.ResidentialAddress.City,
                                    Street = acc.PersonalInformation.ResidentialAddress.Street,
                                    HomeNumber = acc.PersonalInformation.ResidentialAddress.HomeNumber,
                                    ApartmentNumber = acc.PersonalInformation.ResidentialAddress.ApartmentNumber
                                }
                            }
                        };
                        accountDtosList.Add(accountDto);
                    }
                    else
                    {
                        var accountDto = new AccountDto
                        {
                            UserName = acc.UserName,
                            PersonalInformation = new PersonalInformationDto
                            {
                                Name = acc.PersonalInformation.Name,
                                Surname = acc.PersonalInformation.Surname,
                                PersonalCode = acc.PersonalInformation.PersonalCode,
                                Phone = acc.PersonalInformation.Phone,
                                Email = acc.PersonalInformation.Email,
                                ResidentialAddress = null
                            }
                        };
                        accountDtosList.Add(accountDto);
                    }
                }
                else
                {
                    var accountDto = new AccountDto
                    {
                        UserName = acc.UserName,
                       PersonalInformation = null
                    };
                    accountDtosList.Add(accountDto);
                }
            }
            return accountDtosList;


            //var newList = _context.Accounts.Select(x => new AccountDto()
            //{
            //    UserName = x.UserName,
            //    PersonalInformation = new PersonalInformationDto
            //    {
            //        Name = x.PersonalInformation.Name,
            //        Surname = x.PersonalInformation.Surname,
            //        PersonalCode = x.PersonalInformation.PersonalCode,
            //        Phone = x.PersonalInformation.Phone,
            //        Email = x.PersonalInformation.Email,
            //        ResidentialAddress = new ResidentialAddressDto
            //        {
            //            City = x.PersonalInformation.ResidentialAddress.City,
            //            Street = x.PersonalInformation.ResidentialAddress.Street,
            //            HomeNumber = x.PersonalInformation.ResidentialAddress.HomeNumber,
            //            ApartmentNumber = x.PersonalInformation.ResidentialAddress.ApartmentNumber,
            //        }
            //    }
            //});
            //return newList;
        }
        public IEnumerable<string> GetUsersName()
        {
            var accountsNamesList = _context.Accounts.Select(a => a.UserName);
            return accountsNamesList;

        }


        //var acc = _context.Accounts.SelectMany(u => u.PersonalInformation);
        //var accounts = _context.Accounts.SelectMany(u => u.UserName);
        //return accounts;
        //return _context.Accounts.SelectMany(i => i.UserName);
        //}

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
            //var userFromDb = _context.Accounts.FirstOrDefault(p => p.Id == currentUserId);
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
            //var userFromDb = _context.Accounts.FirstOrDefault(p => p.Id == currentUserId);

            var account = _context.Accounts.Include(b => b.PersonalInformation).ThenInclude(b => b.ResidentialAddress)
                .FirstOrDefault(p => p.Id == currentUserId);
            return account.PersonalInformation;

        }
    }
  
    //public PersonalInformation getPersonalInformationById(int accountId)
    //{
    //    var account = _context.Accounts.Include(b => b.PersonalInformation).ThenInclude(b => b.ResidentialAddress)
    //        .FirstOrDefault(p => p.Id == accountId);
    //    return account.PersonalInformation;
    //}
}
