using AssetManagement.Helper;
using AssetManagement.Models;
using AssetManagementWeb.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AssetManagementWeb.Services
{
    public class HardwareService
    {

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ISession _session;
        public string token { get; set; }
        public HardwareService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _session = _contextAccessor.HttpContext.Session;
            token = _session.GetString("admintoken");
            HelperUrl helper = new HelperUrl();
        }

        public async Task<List<AssetModel>> GetHardware()
        {
            HelperUrl helper = new HelperUrl();
            List<AssetModel> hardware = new List<AssetModel>();

            HttpClient client = helper.Initaial();

            HttpResponseMessage response = await client.GetAsync("/Hardware/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                hardware = JsonConvert.DeserializeObject<List<AssetModel>>(result);
            }
            return (hardware);
        }
        public bool AddHardware(HardwareModel hardware)
        {
            HelperUrl helper = new HelperUrl();
            HttpClient client = helper.Initaial();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);

            var postTask = client.PostAsJsonAsync<HardwareModel>("/Hardware/Add", hardware);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
               
            }

            return false;
        }
        public bool DeleteHardware(int id)
        {
            HelperUrl helper = new HelperUrl();
            HttpClient client = helper.Initaial();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);

            var deleteTask = client.DeleteAsync($"/Hardware/Delete/{id}");
            deleteTask.Wait();
            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
              
            }
          
            return false;
        }
        public AssetModel SearchHardware(int id)
        {
            AssetModel asset = null;
            HelperUrl helper = new HelperUrl();
            HttpClient client = helper.Initaial();

            var responseTask = client.GetAsync("Hardware/Search?id=" + id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync().Result;
                asset = JsonConvert.DeserializeObject<AssetModel>(readTask);

            }
            return asset;
        }
        public bool EditHardware(AssetModel asset)
        {
            HelperUrl helper = new HelperUrl();
            HttpClient client = helper.Initaial();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);

            var putTask = client.PutAsJsonAsync<AssetModel>($"/Hardware/Update/{asset.AssetId}", asset);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return true;

            }
            return false;
        }

    }
}
