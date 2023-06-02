namespace DataAccessLayer.Interfaces
{
    public interface IHardware
    {
        bool Delete(int assetId);
        IEnumerable<Asset> GetAll();
        Asset GetById(int assetId);
        void Insert(Asset asset);
        void Save();

        int GetCategoryId(string assetCategory);
    }
}
