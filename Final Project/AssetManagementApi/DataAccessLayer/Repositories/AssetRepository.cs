namespace DataAccessLayer.Repositories
{
    public class AssetRepository : IAssign
    {

		private readonly AssetManagmentContext database;
		public AssetRepository(AssetManagmentContext database)
		{
			this.database = database;
		}

		public bool Delete(int assetId)
		{
			AssetAssignment assetAssignment = database.AssetAssignments.Find(assetId);
			if (assetAssignment != null)
			{
				database.AssetAssignments.Remove(assetAssignment);
				return true;
			}
			return false;
		}

		public IEnumerable<AssetAssignment> GetAll()
		{
			return database.AssetAssignments.ToList();
		}

		public AssetAssignment GetById(int assetId)
		{
			return database.AssetAssignments.Find(assetId);
		}
		public void Insert(AssetAssignment assetAssignment)
		{
			database.AssetAssignments.Add(assetAssignment);
		}

		public void Save()
		{
			database.SaveChanges();
		}


        public AssetAssignment GetRequestAsset(int assetId , int userId)
        {
            var result  = (from asset in database.AssetAssignments
                          where asset.AssetId == assetId && asset.UserId == userId  && asset.Status == "Request"
                          select asset).FirstOrDefault();
            return result;

          
        }

        public AssetAssignment GetIssuedAsset(int assetId, int userId)
        {
            var result = (from asset in database.AssetAssignments
                          where asset.AssetId == assetId && asset.UserId == userId && asset.Status == "Issued"
                          select asset).FirstOrDefault();
            return result;

        }


    }
}
