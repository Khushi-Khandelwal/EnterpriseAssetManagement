﻿namespace AssetManagementApi.Models
{
    public class AssetClass
    {
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Book Name is not Valid")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Author Name is not Valid")]
        [StringLength(50)]
        public string Source { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Incorrect Date Format")]
        public DateTime Date { get; set; }
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Genre is not Valid")]
        [StringLength(50)]
        public string Type { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity should be greater than 0")]
        public int Quantity { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price should be equal to or greater than 1 ")]
        public int Price { get; set; }
    }
}