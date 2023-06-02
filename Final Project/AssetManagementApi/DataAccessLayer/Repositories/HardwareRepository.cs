namespace DataAccessLayer.Repositories
{
    public class HardwareRepository : IHardware
    {
		private readonly AssetManagmentContext database;
		public HardwareRepository(AssetManagmentContext database)
		{
			this.database = database;
		}

		public bool Delete(int hardwareId)
		{
			var result = (from asset in database.Assets
						  join category in database.AssetCategories
						  on asset.CategoryId equals category.CategoryId
						  where category.Name == "Hardware" && asset.AssetId == hardwareId
						  select asset).FirstOrDefault();

			if (result != null)
			{
				database.Assets.Remove(result);
				return true;
			}
			return false;
		}

		public IEnumerable<Asset> GetAll()
		{
			var result = (from asset in database.Assets
						  join category in database.AssetCategories
						  on asset.CategoryId equals category.CategoryId
						  where category.Name == "Hardware"
						  select asset).ToList();
			return result;

		}
		public Asset GetById(int hardwareId)
		{
			var result = (from asset in database.Assets
						  join category in database.AssetCategories
						  on asset.CategoryId equals category.CategoryId
						  where category.Name == "Hardware" && asset.AssetId == hardwareId
						  select asset).FirstOrDefault();
			return result;
		}

		public void Save()
		{
			database.SaveChanges();
		}

		public void Insert(Asset asset)
		{
			database.Assets.Add(asset);
		}
		public int GetCategoryId(string assetCategory)
		{
			int result = (from category in database.AssetCategories
						  where category.Name == assetCategory
						  select category.CategoryId).FirstOrDefault();
			return result;
		}
		
	}
}
