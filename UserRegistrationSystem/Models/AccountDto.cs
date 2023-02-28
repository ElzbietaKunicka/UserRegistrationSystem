using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserRegistrationSystem.Models
{
    public class AccountDto
    {
        public int Id { get; set; }

        [MaxLength(25, ErrorMessage = 
            "Username must be 25 characters or less"), MinLength(3)]
        public string UserName { get; set; }
        public PersonalInformationDto PersonalInformation { get; set; }
    }
}
