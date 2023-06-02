using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IBook
    {
        bool Delete(int assetId);
        IEnumerable<Asset> GetAll();
        Asset GetById(int assetId);
        void Insert(Asset asset);
        void Save();

        int GetCategoryId(string assetCategory);
    }
}
