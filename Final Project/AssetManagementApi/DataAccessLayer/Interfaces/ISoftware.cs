namespace DataAccessLayer.Interfaces
{
    public interface ISoftware
    {
        bool Delete(int assetId);
        IEnumerable<Asset> GetAll();
        Asset GetById(int assetId);
        void Insert(Asset asset);
        void Save();

        int GetCategoryId(string assetCategory);
    }
}
