using AssetManagement.Helper;
using AssetManagement;
using Newtonsoft.Json;
using Microsoft.Extensions.Primitives;

namespace AssetManagementWeb.Services
{
    public class AssetService
    {
        public async Task<List<UserDTO>> GetAssets()
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

        public List<UserDTO> AllRequestAsset()
        {
            List<UserDTO> assets = GetAssets().Result;
            List<UserDTO> assetList = new List<UserDTO>();

            /*   if (filter == "1")
               {
                   return assetList = assets.Where(i => i.AssetType == "Book" && i.Status == "Request").ToList();
               }
               if (filter == "2")
               {
                   return assetList = assets.Where(i => i.AssetType == "Software" && i.Status == "Request").ToList();
               }
               if (filter == "3")
               {
                   return assetList = assets.Where(i => i.AssetType == "Hardware" && i.Status == "Request").ToList();
               }*/

            return assetList = assets.Where(i => i.Status == "Request").ToList();
        }

        public List<UserDTO> RequestAsset(string filter)
        {
            List<UserDTO> assets = GetAssets().Result;
            List<UserDTO> assetList = new List<UserDTO>();

            if (filter == "1")
            {
                return assetList = assets.Where(i => i.AssetType == "Book" && i.Status == "Request").ToList();
            }
            if (filter == "2")
            {
                return assetList = assets.Where(i => i.AssetType == "Software" && i.Status == "Request").ToList();
            }
            if (filter == "3")
            {
                return assetList = assets.Where(i => i.AssetType == "Hardware" && i.Status == "Request").ToList();
            }

            return assetList;
        }


        public async Task<List<UserDTO>> History()
        {
            List<UserDTO> assets = new List<UserDTO>();
            HelperUrl helper = new HelperUrl();
            HttpClient client = helper.Initaial();
            HttpResponseMessage response = await client.GetAsync("/User/GetRequestAsset");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                assets = JsonConvert.DeserializeObject<List<UserDTO>>(result);
            }
            return (assets);
        }

        public List<UserDTO> Search(int id)
        {
            List<UserDTO> assets = GetAssets().Result;
            List<UserDTO> userAssets = new List<UserDTO>();
            userAssets = assets.Where(i => i.UserId == id && i.Status == "Request").ToList();
            return userAssets;
        }
        public List<UserDTO> AllSearch(int id , String filter)
        {
            List<UserDTO> assets = GetAssets().Result;
            List<UserDTO> userAssets = new List<UserDTO>();
            if (filter == "1")
            {
                return userAssets = assets.Where(i => i.UserId == id && i.Status == "Request" && i.AssetType== "Book").ToList();
            }
            if (filter == "2")
            {
                return userAssets = assets.Where(i => i.UserId == id && i.Status == "Request" && i.AssetType == "Software").ToList();
            }
            if (filter == "3")
            {
                return userAssets = assets.Where(i => i.UserId == id && i.Status == "Request" && i.AssetType == "Hardware").ToList();
            }

           // userAssets = assets.Where(i => i.UserId == id && i.Status == "Request").ToList();
            return userAssets;
        }

        public List<UserDTO> ReturnAsset(int id , string filter)
        {
            List<UserDTO> assets = GetAssets().Result;
            List<UserDTO> userAssets = new List<UserDTO>();
            
            userAssets = assets.Where(i => i.UserId == id && i.Status == "Issued").ToList();


            if (filter == "1")
            {
                return userAssets = assets.Where(i => i.UserId == id && i.Status == "Issued" && i.AssetType=="Book").ToList();
            }
            if (filter == "2")
            {
                return userAssets = assets.Where(i => i.UserId == id && i.Status == "Issued" && i.AssetType == "Software").ToList();
            }
            if (filter == "3")
            {
                return userAssets = assets.Where(i => i.UserId == id && i.Status == "Issued" && i.AssetType == "Hardware").ToList();
            }

            return userAssets;
        }

        public List<UserDTO> AllReturnAsset(int id)
        {
            List<UserDTO> assets = GetAssets().Result;
            List<UserDTO> userAssets = new List<UserDTO>();

            userAssets = assets.Where(i => i.UserId == id && i.Status == "Issued").ToList();

/*
            if (filter == "1")
            {
                return userAssets = assets.Where(i => i.UserId == id && i.Status == "Issued" && i.AssetType == "Book").ToList();
            }
            if (filter == "2")
            {
                return userAssets = assets.Where(i => i.UserId == id && i.Status == "Issued" && i.AssetType == "Software").ToList();
            }
            if (filter == "3")
            {
                return userAssets = assets.Where(i => i.UserId == id && i.Status == "Issued" && i.AssetType == "Hardware").ToList();
            }*/

            return userAssets;
        }



    }
}
