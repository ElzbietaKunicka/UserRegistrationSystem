using System.ComponentModel.DataAnnotations.Schema;

namespace UserRegistrationSystem.Dto
{
    public class PersonalInformationDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PersonalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ResidentialAddressDto ResidentialAddress { get; set; }
        //[ForeignKey("ResidentialAddressId")]
       //public int ResidentialAddressId { get; set; }
    }
}
