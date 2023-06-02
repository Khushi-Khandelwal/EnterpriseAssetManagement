using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
