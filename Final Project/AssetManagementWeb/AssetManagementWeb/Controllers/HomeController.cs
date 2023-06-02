using AssetManagement.Helper;
using AssetManagement.Models;
using AssetManagementWeb.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AssetManagement.Controllers
{
    public class HomeController : Controller
    {
        HelperUrl helper = new HelperUrl();
        private readonly HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        public const string sessionToken = "admintoken";

        public IActionResult Index()
        {
            return View();
        }
      
        public IActionResult Admin()
        {
            return View();
        }
       
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login (Login admin)
        {
       
            HttpClient client = helper.Initaial();
            var postTask = client.PostAsJsonAsync<Login>("/Admin/AdminLogin", admin);
            postTask.Wait();

            var result = postTask.Result;
            if(result.IsSuccessStatusCode)
            {
                TempData["message"] = " ";
                var res = result.Content.ReadAsStringAsync().Result;
                HttpContext.Session.SetString(sessionToken, res);
                return RedirectToAction("Admin","Home");
            }
            else
            {
                TempData["message"] = "Invalid Id or Password";
                return View("Login");
            }
           
        }

       
    }
}