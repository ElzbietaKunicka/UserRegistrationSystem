using System.ComponentModel.DataAnnotations;

namespace UserRegistrationSystem.Models
{
    public class ResidentialAddressDto
    {
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = 
            "Street is required")]
        [MaxLength(35, ErrorMessage = 
            "Street cannot be longer than 255 characters.")]
        [MinLength(3, ErrorMessage = 
            "Street cannot be less than 3 characters.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Home number is required")]
        public int HomeNumber { get; set; }
        [Required(ErrorMessage = "Apartment Number is required")]
        public int ApartmentNumber { get; set; }
    }
}
