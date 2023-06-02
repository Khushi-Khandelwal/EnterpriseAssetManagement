using AssetManagement.Helper;
using AssetManagement.Models;
using DataAccessLayer.Entities;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Controllers
{
    public class UserController : Controller
    {
        HelperUrl helper = new HelperUrl();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginModel login)
        {
            HttpClient client = helper.Initaial();
            var response = client.PostAsJsonAsync<LoginModel>("User/Login", login);
            response.Wait();
            var consume = response.Result;
            if (consume.IsSuccessStatusCode)
            {
                TempData["userMessage"] = " ";
                return RedirectToAction("UserPage");
            }
            TempData["userMessage"] = "Invalid Details"; 
            return RedirectToAction("Index", "User");
        }
        public IActionResult UserPage()
        {
			UserHelper userHelper = new UserHelper();
			ViewBag.BookCategory = userHelper.GetBookCategory();
			ViewBag.SoftwareCategory = userHelper.GetSoftwareCategory();
			ViewBag.HardwareCategory = userHelper.GetHardwareCategory();

			return View();
		
        }
        [HttpPost]
        public IActionResult UserPage(UserDTO user)
        {
			if (user.AssetType == "book")
			{
				string bookCategory = user.BookCategory;
				UserHelper userHelper = new UserHelper();
				List<string> books = userHelper.GetBookCategory();
				TempData["BookList"] = books; 
				return RedirectToAction("BookRequest");
			}
			if (user.AssetType == "software")
			{
				string softwareCategory = user.SoftwareCategory;
				return View();
			}
			if (user.AssetType == "hardware")
			{
				string hardwareCategory = user.HardwareCategory;
				return View();
			}

			return View();
		
        }

        public IActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserRegister(UserModel user)
        {

            HttpClient client = helper.Initaial();
            var postTask = client.PostAsJsonAsync<UserModel>("/User/Add", user);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)

            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, " Detail is Not Valid");

            return View(user);
        }

        public IActionResult BookRequest()
        {
			 List<string> booklist = TempData["BookList"] as List<string>;

            ViewBag.ListOfStrings = booklist;
            return View();
        }

    }
  
}
