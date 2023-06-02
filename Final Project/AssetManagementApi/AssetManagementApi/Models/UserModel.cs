namespace AssetManagementApi.Models
{
    public class UserModel
    {

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name must be at least 2 characters long")]
        public string Name { get; set; }

        [Required]
		[DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
		public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must be a 10 digit number")]
        public long PhoneNumber { get; set; }
      

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, and one digit")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }


	}
}
