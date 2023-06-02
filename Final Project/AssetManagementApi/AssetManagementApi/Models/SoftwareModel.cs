namespace AssetManagementApi.Models
{
    public class SoftwareModel
    {
		[Required]
		[RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Software Name is not Valid")]
		[StringLength(50)]
		public string SoftwareName { get; set; }
		[Required]
		[RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Type of Software is not Valid")]
		[StringLength(50)]
		public string TypeOfSoftware { get; set; }
		
		[Required]
		[DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
		public DateTime DateOfExpiry { get; set; }
		[Required]
		public string LicenceNumber { get; set; }
		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Quantity should be greater than 0")]
		public int Quantity { get; set; }

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Price should be greater than 1")]
		public int Price { get; set; }

		
	}
}
