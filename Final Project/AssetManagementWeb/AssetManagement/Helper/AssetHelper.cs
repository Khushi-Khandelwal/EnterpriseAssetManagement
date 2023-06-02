using AssetManagement.Helper;
using Newtonsoft.Json;

namespace AssetManagement
{
    public class AssetHelper
    {
        private async Task<List<UserDTO>> GetAssets()
        {
            HelperUrl helper = new HelperUrl();

            List<UserDTO> assets = new List<UserDTO>();
            HttpClient client = helper.Initaial();

            HttpResponseMessage res = await client.GetAsync("/User/GetAssetDetail");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                assets = JsonConvert.DeserializeObject<List<UserDTO>>(result);
            }
            return (assets);
        }

        public List<UserDTO> Get(string filter)
        {
            List<UserDTO> assets = GetAssets().Result;
            List<UserDTO> assetList = new List<UserDTO>();

            if (filter == "1")
            {
                return assetList = assets.Where(i => i.AssetType == "Book").ToList();
            }
            if (filter == "2")
            {
                return assetList = assets.Where(i => i.AssetType == "Software").ToList();
            }
            if (filter == "3")
            {
                return assetList = assets.Where(i => i.AssetType == "Hardware").ToList();
            }

            return assetList;
        }
    }
}
