using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
	public class Asset
	{ 
		[Key]
		public int AssetId {  get; set; }

		[StringLength(50)]
		public string Name { get; set; }
		[StringLength(50)]
		public string Source { get; set; }
		[Column(TypeName = "date")]
		public DateTime? Date { get ; set; }
		
		[StringLength(50)]
		public string Type { get; set; }

		public int? Quantity { get; set; }

		public int? Price { get; set; }

		public int CategoryId { get; set; }

		[ForeignKey("CategoryId")]
		public virtual AssetCategory AssetCategory { get; set; }

		public ICollection<AssetAssignment> AssetAssignments { get; set; }

	}
}
