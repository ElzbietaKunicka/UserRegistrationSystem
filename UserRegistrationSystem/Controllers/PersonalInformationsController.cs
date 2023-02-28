using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using UserRegistrationSystem.DAL;
using UserRegistrationSystem.Models;

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
        public ActionResult PostItem([FromBody] PersonalInformationDto personalInformationToAdd)
        {
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var currentUserIdInt = int.Parse(userIdStr);
            if (ModelState.IsValid)
            {
                _personalInformationList.AddNewPersonalInformation(currentUserIdInt, personalInformationToAdd);
                return Ok();
            }
            return BadRequest(ModelState);
            //_personalInformationList.AddNewPersonalInformation(currentUserIdInt, personalInformationToAdd);
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
        public ActionResult GetInformationByID(int id)
        {
            var userInfoById = _personalInformationList.getById(id);
            if (userInfoById == null)
            {
                return Content("Account ID does not exist");
            }
            return Ok(userInfoById);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _personalInformationList.DeleteAccountById(id);
        }
    }
}
