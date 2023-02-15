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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpPost]
        public void PostItem([FromBody]PersonalInformationDto personalInformationToAdd)
        {
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var currentUserIdInt = int.Parse(userIdStr);
           _personalInformationList.AddNewPersonalInformation(currentUserIdInt, personalInformationToAdd);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpPut]
        public void UpdateItem([FromBody]PersonalInformationDto personalInformationToAdd)
        {
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var currentUserIdInt = int.Parse(userIdStr);
            _personalInformationList.UpdatePersonalInformation(currentUserIdInt, personalInformationToAdd);
        }
        ////[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        //[HttpGet]
        //public PersonalInformation GetPersonalInformationByIdI(int accountId)
        //{
        //    return _personalInformationList.getPersonalInformationById(accountId);

        //}
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpGet]
        public List<PersonalInformationDto> GetPersonalInformationByIdI(int personalInfoId)
        {
            return _personalInformationList.getPersonalInformationById(personalInfoId);
            // selecta adr requestas and response...
        }
    }
}
