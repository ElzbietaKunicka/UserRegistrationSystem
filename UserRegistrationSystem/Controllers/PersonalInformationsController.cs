using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRegistrationSystem.DAL;
using UserRegistrationSystem.Dto;

namespace UserRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalInformationsController : ControllerBase
    {
        private readonly IPersonalInformationList _personalInformationList;
        public PersonalInformationsController(IPersonalInformationList personalInformationList)
        {
            _personalInformationList = personalInformationList;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        [HttpPost]
        public void PostItem([FromBody] PersonalInformationDto personalInformationToAdd)
        {
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var currentUserIdInt = int.Parse(userIdStr);
            
            _personalInformationList.AddNewPersonalInformation(currentUserIdInt, personalInformationToAdd);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        [HttpPut]
        public void UpdateItem([FromBody] PersonalInformationDto personalInformationToAdd)
        {
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var currentUserIdInt = int.Parse(userIdStr);
            _personalInformationList.UpdatePersonalInformation(currentUserIdInt, personalInformationToAdd);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        [HttpGet("PersonalInformationId")]
        public int? GetPersonalInformationIdByCurrentUserId()
        {
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var currentUserIdInt = int.Parse(userIdStr);
            return _personalInformationList.getPersonalInformationIdByCurrentUser(currentUserIdInt);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        [HttpGet("AccountsIdAndUsernames")]
        public IEnumerable<AccountDto> GetUsersIdAndUsernames()
        {
            return _personalInformationList.GetUsersIdAndUsernames();
        }
        

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        [HttpGet("CurrentUserInfo")]
        public PersonalInformation GetAllInfoAboutCurrentUser()
        {
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var currentUserIdInt = int.Parse(userIdStr);
            return _personalInformationList.GetAllInfoAboutCurrentUser(currentUserIdInt);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        [HttpGet("CurrentUserName")]
        public string GetCurrentUserName()
        {
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var currentUserIdInt = int.Parse(userIdStr);
            return _personalInformationList.GetCurrentUserName(currentUserIdInt);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        [HttpGet("GetById/{id?}")]
        public AccountDto GetInformationByID(int id)
        {
            return _personalInformationList.getById(id);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Admin")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _personalInformationList.DeleteAccountById(id);
        }

        ////[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        //[HttpGet]
        //public PersonalInformation GetPersonalInformationByIdI(int accountId)
        //{
        //    return _personalInformationList.getPersonalInformationById(accountId);

        //}
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        //[HttpGet] // want todo pagal accountId o ne pagal personal info
        //public List<PersonalInformationDto> GetPersonalInformationById(int personalInfoId)
        //{
        //    return _personalInformationList.getPersonalInformationById(personalInfoId);
        //    // selecta adr requestas and response...
        //}

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        //[HttpGet("PersonalInformationByUserName")] 
        //public IEnumerable<AccountDto> GetAccountsPersonalInformationByName(string accountsName)
        //{
        //    return _personalInformationList.getAccountsInformationByName(accountsName);
        //}

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        ////[ProducesDefaultResponseType(typeof(AccountDto))]
        //[HttpGet("AllUsersInfo")]
        //public IEnumerable<AccountDto> GetAlLInfoAboutUsers()
        //{
        //    //var userIdStr = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        //    //var currentUserIdInt = int.Parse(userIdStr);
        //    return _personalInformationList.GetAllInfoAboutUsers();
        //}

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        //[HttpGet("AccountsNames")]
        //public IEnumerable<string> GetAlLUsersNames()
        //{
        //    return _personalInformationList.GetUsersName();
        //}


    }
}
