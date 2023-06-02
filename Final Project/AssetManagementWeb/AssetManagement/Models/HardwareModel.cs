using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Models
{
    public class HardwareModel
    {

        public int HardwareId { get; set; }

        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Hardware Name is not Valid")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Manufacture Name is not Valid")]
        [StringLength(50)]
        public string Manufacturer { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Warranty(in Months) should be greater than or equal to 0 or less than 100")]
        public int Warranty { get; set; }
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Category Name is not Valid")]
        [StringLength(50)]
        public string Category { get; set; }
        [Required]
        [Range(1, int.MaxValue , ErrorMessage = "Quantity should be greater than 0")]
        public int Quantity { get; set; }
    }
}
