using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementWeb.Models
{
    public class RegisterUser
    {
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "User Name is not Valid")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [StringLength(20)]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Phone Number is Not Valid")]
        public long PhoneNumber { get; set; }

        [Required]
       
        public string Password { get; set; }
    }
}
