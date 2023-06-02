using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations;

namespace AssetManagement
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string AssetType { get; set; }
        public string AssetName { get; set; }

        public long PhoneNumber { get; set; }

        public int Price { get; set; }

        public string Status { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
        public DateTime? AssignDate { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
        public DateTime? UnAssignDate { get; set; }

        public int UserId { get; set; }
           
        public int AssetId { get; set; }

       
    }
}
