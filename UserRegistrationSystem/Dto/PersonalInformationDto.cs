using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserRegistrationSystem.Dto
{
    public class PersonalInformationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string PersonalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ResidentialAddressDto ResidentialAddress { get; set; }
       
    }
}
