using System.ComponentModel.DataAnnotations;

namespace AssetManagement
{
    public class AssetDTO
    {
        [Required]
        [Range(1, int.MaxValue , ErrorMessage = "User Id is not Negative or Zero") ]
        public int UserId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Asset Id is not Negative or Zero")]
        public int AssetId { get; set; }
    }
}
