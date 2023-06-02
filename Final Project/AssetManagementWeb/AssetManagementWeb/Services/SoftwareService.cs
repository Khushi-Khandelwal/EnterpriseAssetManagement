using AssetManagement.Helper;
using AssetManagement.Models;
using AssetManagementWeb.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AssetManagementWeb.Services
{
    public class SoftwareService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ISession _session;
        public string token { get; set; }
        public SoftwareService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _session = _contextAccessor.HttpContext.Session;
            token = _session.GetString("admintoken");
            HelperUrl helper = new HelperUrl();
        }

        public async Task<List<AssetModel>> GetSoftware()
        {
            HelperUrl helper = new HelperUrl();
            List<AssetModel> software = new List<AssetModel>();

            HttpClient client = helper.Initaial();

            HttpResponseMessage response = await client.GetAsync("/Software/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                software = JsonConvert.DeserializeObject<List<AssetModel>>(result);
            }
            return (software);
        }

        public bool AddSoftware(SoftwareModel software)
        {
            HelperUrl helper = new HelperUrl();
            HttpClient client = helper.Initaial();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);

            var postTask = client.PostAsJsonAsync<SoftwareModel>("/Software/Add", software);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
               
            }

            return false;
        }
        public bool DeleteSoftware(int id)
        {
            HelperUrl helper = new HelperUrl();
            HttpClient client = helper.Initaial();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);

            var deleteTask = client.DeleteAsync($"/Software/Delete/{id}");
            deleteTask.Wait();
            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public AssetModel SearchSoftware(int id)
        {
            AssetModel asset = null;
            HelperUrl helper = new HelperUrl();
            HttpClient client = helper.Initaial();
            var responseTask = client.GetAsync("Software/Search?id=" + id.ToString());
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync().Result;
                asset = JsonConvert.DeserializeObject<AssetModel>(readTask);

            }
            return asset;
        }
        public bool EditSoftware(AssetModel asset)
        {
            HelperUrl helper = new HelperUrl();
            HttpClient client = helper.Initaial();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", parameter: token);

            var putTask = client.PutAsJsonAsync<AssetModel>($"/Software/Update/{asset.AssetId}", asset);
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
