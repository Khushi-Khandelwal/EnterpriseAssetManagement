namespace AssetManagementApi.Models
{
    public class AssetModel
    {

        [Required]
        public int UserId { get; set; }
        [Required]
        public int AssetId { get; set; }


    }
}
