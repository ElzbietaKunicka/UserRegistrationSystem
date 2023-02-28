using System.ComponentModel.DataAnnotations;

namespace UserRegistrationSystem.Models
{
    public class AuthRequestDto
    {
        [Required]
        [MaxLength(25, ErrorMessage =
           "UserName cannot be greater than 25")]
        [MinLength(3, ErrorMessage =
           "UserName cannot be less than 3")]
        public string UserName { get; set; }
        [MaxLength(25, ErrorMessage =
   "Password cannot be greater than 25")]
        [MinLength(3, ErrorMessage =
   "Password cannot be less than 3")]
        [Required]
        public string Password { get; set; }
    }
}
