using AssetManagement.Helper;
using AssetManagement.Models;
using AssetManagementWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssetManagement.Controllers
{
    public class AdminController : Controller
    {
        HelperUrl helper = new HelperUrl();
        public async Task<IActionResult> Details()
        {
            AssetService assetService = new AssetService();
            return View(assetService.GetAssets().Result);
        }
        [HttpPost]
        public IActionResult Details(string filter)
        {
            AssetService asset = new AssetService();
            return View(asset.Get(filter));
        }

        
        public IActionResult Request()
        {
            AssetService asset = new AssetService();
            return View(asset.AllRequestAsset());
        }
        [HttpPost]
        public IActionResult Request(string filter)
        {
            TempData["RequestFilter"] = filter;
            TempData.Keep();
            AssetService asset = new AssetService();
            return View(asset.RequestAsset(filter));
        }

        [HttpGet("yup")]
        public IActionResult Accept(UserDTO user )
        {
           AssetDTO asset = new AssetDTO(); 
            asset.AssetId = user.AssetId;
            asset.UserId = user.UserId;

            HttpClient client = helper.Initaial();
            var postTask = client.PostAsJsonAsync<AssetDTO>("/Software/AcceptSoftwareAsset", asset);
            postTask.Wait();

            /* if (TempData["RequestFilter"].ToString() == "2")
             {

                 var postTask = client.PostAsJsonAsync<AssetDTO>("/Software/AcceptSoftwareAsset", asset);
                 postTask.Wait();

             }
             if (TempData["RequestFilter"].ToString() == "1")
             {

                 var postTask = client.PostAsJsonAsync<AssetDTO>("/Book/AcceptBookAsset", asset);
                 postTask.Wait();

             }

             if (TempData["RequestFilter"].ToString() == "3")
             {
                 var postTask = client.PostAsJsonAsync<AssetDTO>("/Hardware/AcceptHardwareAsset", asset);
                 postTask.Wait();

             }*/

            return RedirectToAction("Details");

        }

        [HttpGet("hi")]
        public IActionResult Reject(UserDTO user)
        {
            AssetDTO asset = new AssetDTO();
            asset.AssetId = user.AssetId;
            asset.UserId = user.UserId;

            HttpClient client = helper.Initaial();

            if (TempData["RequestFilter"].ToString() == "2")
            {

                var postTask = client.PostAsJsonAsync<AssetDTO>("/Software/RejectSoftwareAsset", asset);
                postTask.Wait();

            }
            if (TempData["RequestFilter"].ToString() == "1")
            {

                var postTask = client.PostAsJsonAsync<AssetDTO>("/Book/RejectBookAsset", asset);
                postTask.Wait();

            }
           
            if (TempData["RequestFilter"].ToString() == "3")
            {
                var postTask = client.PostAsJsonAsync<AssetDTO>("/Hardware/RejectHardwareAsset", asset);
                postTask.Wait();

            }

            return RedirectToAction("Details");
        }
    }
}
