using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
	public class UserDetail
	{
		[Key]
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
		public ICollection<AssetAssignment> AssetAssignments { get; set; }
	}
}
