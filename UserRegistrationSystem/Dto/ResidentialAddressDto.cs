using System.ComponentModel.DataAnnotations;

namespace UserRegistrationSystem.Dto
{
    public class ResidentialAddressDto
    {
        public string City { get; set; }
        public string Street { get; set; }
        [Required]
        public int HomeNumber { get; set; }
        public int ApartmentNumber { get; set; }
    }
}
