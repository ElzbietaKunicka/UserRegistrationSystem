using System.ComponentModel.DataAnnotations.Schema;

namespace UserRegistrationSystem.Dto
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        //public string Password { get; set; }
        public PersonalInformationDto PersonalInformation { get; set; }
    }
}
