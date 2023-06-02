using AssetManagement.Helper;
using AssetManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AssetManagement.Controllers
{
    public class HomeController : Controller
    {
        HelperUrl helper = new HelperUrl();
      
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminRoleValidation()
        {
            return View();
        }
        
        public IActionResult AdminValidation(AdminModel admin)
        {
            if(admin.UserName=="Admin" && admin.Password=="admin123")
            {
                TempData["message"] = " ";
                return RedirectToAction("Admin", "Home");
            }
            TempData["message"] = "invalid Password or Password";
            return RedirectToAction("AdminRoleValidation" , "Home");
        }
        public IActionResult Admin()
        {
            return View();
        }
        

        //
/*
        public async Task<IActionResult> Genres()
        {
            List<string> books = new List<string>();
            HttpClient client = helper.Initaial();

            HttpResponseMessage res = await client.GetAsync("/Book/GetAllGenre");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<string>>(result);
            }
            return View(books);
        }*/
/*
        //
        public IActionResult test()
        {
            List<String> l = new List<String>();
                 
            ViewBag.Genres  =   TempData["BookName"];

            return View();
        }

        [HttpPost]
        public IActionResult TestResult(IFormCollection form)
        {
            string a = form["down1"];
            string b = form["down2"];
            string c = form["down3"];

            return Ok("bdcjhabvcdhlav");
        }*/
       
    }
}