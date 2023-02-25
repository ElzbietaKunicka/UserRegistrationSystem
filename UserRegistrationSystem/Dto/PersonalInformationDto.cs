using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserRegistrationSystem.Dto
{
    public class PersonalInformationDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(35, ErrorMessage = "Name cannot be longer than 35 characters.")]
        [MinLength(3, ErrorMessage = "Name cannot be less than 3 characters.")]
       // [MaxLength(35), MinLength(3)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        [MaxLength(35, ErrorMessage = "Surname cannot be longer than 35 characters.")]
        [MinLength(3, ErrorMessage = "Surname cannot be less than 3 characters.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "PersonalCode is required")]
        [MaxLength(11, ErrorMessage = "personal code should be 11 numbers.")]
        [MinLength(11, ErrorMessage = "personal code should be 11 numbers.")]
        public string PersonalCode { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        [MaxLength(12, ErrorMessage = "Phone cannot be longer than 12 characters.")]
        [MinLength(9, ErrorMessage = "Phone cannot be less than 9 characters.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(255, ErrorMessage = "Email cannot be longer than 255 characters.")]
        [MinLength(7, ErrorMessage = "Email cannot be less than 7 characters.")]
        public string Email { get; set; }
        public ResidentialAddressDto ResidentialAddress { get; set; }
       
    }
}
