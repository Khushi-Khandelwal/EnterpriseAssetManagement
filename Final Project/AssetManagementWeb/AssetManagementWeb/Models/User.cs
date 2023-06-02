using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AssetManagementWeb.Models
{
    public class User
    {
        public int UserId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

    }
}
