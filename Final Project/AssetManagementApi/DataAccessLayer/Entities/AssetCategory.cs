using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
	public class AssetCategory
	{
		[Key]
		public int CategoryId { get; set; }
		[StringLength(50)]
		public string Name { get; set; }

		public ICollection<Asset> Assets { get; set; }

	}
}
