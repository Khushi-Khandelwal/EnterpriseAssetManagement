using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class AssetAssignment
	{
		[Key]
		public int Id { get; set; }

		[StringLength(50)]
		public string Status { get; set; }
		[Column(TypeName = "date")]
		public DateTime? AssignDate { get; set; }
		[Column(TypeName = "date")]
		public DateTime? UnAssignDate { get; set; }

		public int UserId { get; set; }

		public int AssetId { get; set; }

		[ForeignKey("UserId")]
		public virtual UserDetail UserDetail { get; set; }

		[ForeignKey("AssetId")]
		public virtual Asset Asset { get; set; }


	}
}
