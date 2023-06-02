using AssetManagement.Helper;
using AssetManagementWeb.Models;
using AssetManagementWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AssetManagement.Controllers
{
    public class UserController : Controller
    {
        HelperUrl helper = new HelperUrl();
        private readonly HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        public const string sessionToken = "usertoken";
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login user)
        {

            HttpClient client = helper.Initaial();

            var postTask = client.PostAsJsonAsync<Login>("/Admin/UserLogin", user);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                TempData["usermessage"] = " ";
                var userResponse = client.PostAsJsonAsync<Login>("/User/GetUserId", user);
                userResponse.Wait();
                var userID = userResponse.Result;
                var userResult = userID.Content.ReadAsStringAsync().Result;
                int storeUserId = JsonConvert.DeserializeObject<int>(userResult);
                TempData["userId"] = storeUserId;

                var res = result.Content.ReadAsStringAsync().Result;
                HttpContext.Session.SetString(sessionToken, res);
                return RedirectToAction("UserHomePage", "User");
            }
            else
            {
                TempData["usermessage"] = "Invalid Id or Password";
                return View("Login");
            }

        }

        public IActionResult UserHomePage()
        {
            return View();
        }

        public async Task<IActionResult> UserHome()
        {
            HelperUrl helper = new HelperUrl();
            List<AssetAssignModel> assets = new List<AssetAssignModel>();
            HttpClient client = helper.Initaial();
            HttpResponseMessage response = await client.GetAsync("/User/GetAllAsset");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                assets = JsonConvert.DeserializeObject<List<AssetAssignModel>>(result);
            }
            return View(assets);
        }
        [HttpPost]
        public async Task<IActionResult> UserHome(string filter)
        {
            HelperUrl helper = new HelperUrl();
            List<AssetAssignModel> assets = new List<AssetAssignModel>();
            List<AssetAssignModel> assetList = new List<AssetAssignModel>();

            TempData["filter"] = filter;
            HttpClient client = helper.Initaial();

            HttpResponseMessage response = await client.GetAsync("/User/GetAllAsset");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                assets = JsonConvert.DeserializeObject<List<AssetAssignModel>>(result);
            }
            if (filter == "1")
            {
                assetList = assets.Where(i => i.CategoryId == 1).ToList();
                return View(assetList);
            }
            if (filter == "2")
            {
                assetList = assets.Where(i => i.CategoryId == 2).ToList();
                return View(assetList);
            }
            if (filter == "3")
            {
                assetList = assets.Where(i => i.CategoryId == 3).ToList();
                return View(assetList);
            }
            return View(assets);
        }


        public IActionResult SubmitRequest(int id)
        {
            HelperUrl helper = new HelperUrl();
            HttpClient client = helper.Initaial();
            AssetDTO assetDTO = new AssetDTO();
            assetDTO.AssetId = id;
            assetDTO.UserId = (int)TempData["userId"];
            if (TempData["filter"].ToString() == "2")
            {


                var postTask = client.PostAsJsonAsync<AssetDTO>("/Software/RequestSoftwareAsset", assetDTO);
                postTask.Wait();

            }
            if (TempData["filter"].ToString() == "1")
            {

                var postTask = client.PostAsJsonAsync<AssetDTO>("/Book/RequestBookAsset", assetDTO);
                postTask.Wait();


            }

            if (TempData["filter"].ToString() == "3")
            {
                var postTask = client.PostAsJsonAsync<AssetDTO>("/Hardware/RequestHardwareAsset", assetDTO);
                postTask.Wait();
            }

            return RedirectToAction("UserHome");

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterUser user)
        {

            HttpClient client = helper.Initaial();
            var postTask = client.PostAsJsonAsync<RegisterUser>("/User/Add", user);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, "User Detail is Not Valid");

            return View(user);

        }

        [HttpGet]
        public IActionResult History()
        {
            int userId = (int)TempData["userId"];
            AssetService service = new AssetService();
            TempData.Keep();
            return View(service.Search(userId));

        }
        [HttpPost]
        public IActionResult History(string filter)
        {
            int userId = (int)TempData["userId"];
            TempData["RequestFilter"] = filter;
            AssetService service = new AssetService();
            TempData.Keep();
            return View(service.AllSearch(userId, filter));

        }

        public IActionResult CancelRequest(UserDTO user)
        {
            AssetDTO asset = new AssetDTO();
            asset.AssetId = user.AssetId;
            asset.UserId = user.UserId;

            HttpClient client = helper.Initaial();

            if (TempData["RequestFilter"].ToString() == "1")
            {

                var postTask = client.PostAsJsonAsync<AssetDTO>("/Book/RejectBookAsset", asset);
                postTask.Wait();

            }
            if (TempData["RequestFilter"].ToString() == "2")
            {

                var postTask = client.PostAsJsonAsync<AssetDTO>("/Software/RejectSoftwareAsset", asset);
                postTask.Wait();

            }
            if (TempData["RequestFilter"].ToString() == "3")
            {
                var postTask = client.PostAsJsonAsync<AssetDTO>("/Hardware/RejectHardwareAsset", asset);
                postTask.Wait();

            }

            return RedirectToAction("History");
        }




        public IActionResult ReturnAsset()
        {
            int id = (int)TempData["userId"];
            TempData.Keep();
            AssetService asset = new AssetService();
            return View(asset.AllReturnAsset(id));
        }

        [HttpPost]
        public IActionResult ReturnAsset(string filter)
        {
            TempData["returnFilter"] = filter;
            AssetService service = new AssetService();
            int id = (int)TempData["userId"];
            TempData.Keep();
            return View(service.ReturnAsset(id, filter));

        }
        [HttpGet]
        public IActionResult Return(UserDTO user)
        {
            AssetDTO asset = new AssetDTO();
            asset.AssetId = user.AssetId;
            asset.UserId = user.UserId;
            HttpClient client = helper.Initaial();
            var postTask = client.PostAsJsonAsync<AssetDTO>("/Software/ReturnSoftwareAsset", asset);
            postTask.Wait();

            //if (TempData["returnFilter"].ToString() == "2")
            //{
            //    var postTask = client.PostAsJsonAsync<AssetDTO>("/Software/ReturnSoftwareAsset", asset);
            //    postTask.Wait();
            //}
            //if (TempData["returnFilter"].ToString() == "1")
            //{
            //    var postTask = client.PostAsJsonAsync<AssetDTO>("/Book/ReturnBookAsset", asset);
            //    postTask.Wait();
            //}
            //if (TempData["returnFilter"].ToString() == "3")
            //{
            //    var postTask = client.PostAsJsonAsync<AssetDTO>("/Hardware/ReturnHardwareAsset", asset);
            //    postTask.Wait();
            //}

            return RedirectToAction("ReturnAsset");

        }



    }

}
