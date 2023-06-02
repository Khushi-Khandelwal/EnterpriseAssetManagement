using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
	public interface IAssign
	{
		bool Delete(int assetId);
		IEnumerable<AssetAssignment> GetAll();
		AssetAssignment GetById(int assetId);
		void Insert(AssetAssignment asset);
		void Save();

		AssetAssignment GetRequestAsset(int assetId, int userId);

		AssetAssignment GetIssuedAsset(int assetId, int userId);

    }
}
