using AssetManagement.Helper;
using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssetManagement.Controllers
{
    public class AdminController : Controller
    {
        public async Task<IActionResult> Details()
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
            return View(assets);
        }
        [HttpPost]
        public IActionResult Details(string filter)
        {
            AssetHelper asset = new AssetHelper();
            return View(asset.Get(filter));
        }
    }
}
