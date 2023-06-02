namespace DataAccessLayer.Repositories
{
    public class BookRepository : IBook
    {
        private readonly AssetManagmentContext database;
        public BookRepository(AssetManagmentContext database)
        {
            this.database = database;
        }

        public bool Delete(int bookId)
        {
            var result = (from asset in database.Assets
                          join category in database.AssetCategories
                          on asset.CategoryId equals category.CategoryId
                          where category.Name == "Book" && asset.AssetId == bookId
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
                          where category.Name == "Book"
                          select asset).ToList();
            return result;

        }

        public Asset GetById(int bookId)
        {
            var result = (from asset in database.Assets
                          join category in database.AssetCategories
                          on asset.CategoryId equals category.CategoryId
                          where category.Name == "Book" && asset.AssetId == bookId
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
