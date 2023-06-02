using AssetManagementView.Helper;
using AssetManagementView.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace AssetManagementView.Controllers
{
    public class BookController : Controller
    {
        BookApi bookapi = new BookApi();

        public async Task<IActionResult> Index()
        {
            List<BookModel> books = new List<BookModel>();
            HttpClient client = bookapi.Initaial();

            HttpResponseMessage res = await client.GetAsync("/Book/GetAll");

            if(res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<BookModel>>(result);
            }
            return View(books);
        }
       
    }
}
