using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace UserRegistrationSystem.Models
{
    [Index(nameof(UserName), IsUnique = true)]
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(25, ErrorMessage = 
            "UserName cannot be greater than 25")]
        [MinLength(3, ErrorMessage = 
            "UserName cannot be less than 3")]
        public string UserName { get; set; }
        [MaxLength(50), MinLength(3)]
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        [ForeignKey("PersonalInformationId")]
        public int? PersonalInformationId { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
    }
}
